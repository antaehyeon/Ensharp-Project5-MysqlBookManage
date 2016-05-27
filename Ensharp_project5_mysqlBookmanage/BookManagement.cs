using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Project2_BookStore
{
    class BookManagement
    {
        private Print print;
        private SharingData sd;
        private Exception exception;
        private Run run;

        private string bookNo;
        private string bookName;
        private string bookAuthor;
        private string bookPrice;
        private string bookQuantity;

        private string id;

        DataSet ds;

        public BookManagement(Run run)
        {
            this.run = run;
            print = new Print(run);
            exception = new Exception();
            sd = SharingData.GetInstance();
        }

        public void registerBookFunction(int mode)
        {
            switch(mode)
            {
                case 0: // 책 번호
                    bookNo = enterBookNoFunction();
                    this.registerBookFunction(1);
                    break;
                case 1: // 책 이름
                    bookName = this.enterBookNameFunction();
                    this.registerBookFunction(2);
                    break;
                case 2: // 책 저자
                    bookAuthor = this.enterBookAuthorFunction();
                    this.registerBookFunction(3);
                    break;
                case 3: // 책 수량
                    bookQuantity = this.enterBookQuantityFunction();
                    this.registerBookFunction(4);
                    break;
                case 4: // 책 가격
                    bookPrice = this.enterBookPriceFunction();
                    break;
            }
            sd.bookInfoInsert(bookNo, bookName, bookAuthor, bookPrice, bookQuantity);
            sd.insertBookRentNo(bookNo);
            print.bookRegisterSuccessMessage();
            run.bookMenu();
        }

        // 책 고유번호를 입력받는 기능
        public string enterBookNoFunction()
        {
            print.enterBookNoMessage();
            bookNo = Console.ReadLine();
            if (bookNo == "b") run.bookMenu();
            if (exception.bookNoCheck(bookNo))
            {
                enterBookNoFunction();
            }
            return bookNo;
        }

        // 책 이름 입력받는 기능
        public string enterBookNameFunction()
        {
            print.enterBookNameMessage();
            bookName = Console.ReadLine();
            if (bookName == "b") run.bookMenu();
            if(exception.bookNameCheck(bookName))
            {
                this.enterBookNameFunction();
            }
            return bookName;
        }

        // 책 저자 입력받는 기능
        public string enterBookAuthorFunction()
        {
            print.enterBookAuthorMessage();
            bookAuthor = Console.ReadLine();
            if (bookAuthor == "b") run.bookMenu();
            if (exception.bookAuthorCheck(bookAuthor))
            {
                this.enterBookAuthorFunction();
            }
            return bookAuthor;
        }

        // 책 수량 입력기능
        public string enterBookQuantityFunction()
        {
            print.enterBookQuantity();
            bookQuantity = Console.ReadLine();
            if (bookQuantity == "b") run.bookMenu();
            if (exception.bookQuantityCheck(bookQuantity))
            {
                this.enterBookQuantityFunction();
            }
            return bookQuantity;
        }

        // 책 가격 입력기능
        public string enterBookPriceFunction()
        {
            print.enterBookPrice();
            bookPrice = Console.ReadLine();
            if (bookPrice == "b") run.bookMenu();
            if (exception.bookPriceCheck(bookPrice))
            {
                this.enterBookPriceFunction();
            }
            else if (string.IsNullOrWhiteSpace(bookPrice))
            {
                bookPrice = "FREE";
            }
            return bookPrice;
        }

        // 책을 찾는 기능
        public void findBookFunction()
        {
            // 책이 아무것도 없을 경우 예외처리
            this.noExistBookFunc();
            // 책을 찾는다
            print.findBookName();
            bookName = Console.ReadLine();
            if (bookName == "b") run.bookMenu();
            //
            this.printBookNameFunc(bookName);
            print.knowEndMessage();
            run.bookMenu();
        }

        // 책 전체출력 기능
        public void printBookFunction()
        {
            Console.Clear();

            this.noExistBookFunc();
            this.printAllBookFunc();
            print.knowEndMessage();
            run.bookMenu();
        }

        // 책 삭제기능
        public void deleteBookFunction()
        {
            // MySql에 책 데이터가 하나도 없으면 예외
            this.noExistBookFunc();
            // Title 출력
            print.deleteBookTitle();
            // 책을 전부 출력시킨 후에
            this.printAllBookFunc();
            // 무엇을 지울지 입력받는다 (책의 번호로)
            print.enterBookNumberForDelete();
            bookNo = Console.ReadLine();
            if (bookNo == "b") run.bookMenu();
            // 만약 그런 책이 존재하면 TRUE
            // 앞에 부정을 줘서 FALSE = 존재하지 않을경우 ERROR
            if (!sd.selectForExists("book", "BookNo", bookNo))
            {
                this.deleteBookFunction();
            }
            // TRUE : 책이 존재한다면 DB에서 Delete
            sd.delete("book", "BookNo", bookNo);
            print.bookDeleteSuccessMessage();
            this.deleteBookFunction();

        }

        // 책 출력부분
        public void printAllBookFunc()
        {
            print.bookTitle();

            // SELECT * FROM BOOK
            ds = sd.selectAll("book");

            // BookData에 넣고 Print Class 쪽으로 BookData를 넘겨줘서 출력
            foreach(DataRow r in ds.Tables[0].Rows)
            {
                sd.BookData.BookNo = Convert.ToString(r["BookNo"]);
                sd.BookData.BookName = Convert.ToString(r["Name"]);
                sd.BookData.BookAuthor = Convert.ToString(r["Author"]);
                sd.BookData.BookPrice = Convert.ToString(r["Price"]);
                sd.BookData.BookQuantity = Convert.ToString(r["Quantity"]);

                print.bookElement(sd.BookData);
                print.bookEndLine();
            }
        } // Method - printBookInfo

        // 책 변경기능
        public void changeBookFunction()
        {
            // 책 데이터가 하나도 없을경우 예외처리
            this.noExistBookFunc();
            // 책 이름을 입력받는다
            print.findBookName();
            bookName = Console.ReadLine();
            if (bookName == "b") run.bookMenu();

            // 아래 함수로 빠짐
            // 뒤로가기를 구현하기 위한 메소드 분할
            modifyBookSelect();
        }

        // 위의 함수와 이어짐
        public void modifyBookSelect()
        {
            // TITLE : 도서수정
            print.modifyBookTitle();
            // 책의 이름을 찾는데
            // 있으면 TRUE
            // 없으면 FALSE 를 반환시켜줌
            // 그러므로 아래의 조건은 책이 없을때
            if (!printBookNameFunc(bookName))
            {
                print.noExistBook();
                changeBookFunction();
            }
            // 수정할 책의 번호를 입력받음
            print.enterBookNumberForModify();
            bookNo = Console.ReadLine();
            if (bookNo == "b") run.bookMenu();
            // 다시 항목을 선택하게 함
            run.bookInfoChangeMenu();
            // bookInfoChangeMenu 의 번호선택에 따라서 아래의 함수들이 호출됨
        }

        // 1 : 책이름 수정
        public void changeNameFunc()
        {
            // TITLE과 책 이름 입력
            print.enterBookForModify(1);
            bookName = Console.ReadLine();
            if (bookName == "b") run.bookInfoChangeMenu();
            // 책 이름에 대한 예외처리
            if(exception.bookNameCheck(bookName))
            {
                this.changeNameFunc();
            }
            sd.update("book", "Name", bookName, "BookNo", bookNo);
            print.successModifyMessage();
        }

        // 책 저자 수정기능
        public void changeAuthorFunc()
        {
            print.enterBookForModify(2);
            bookAuthor = Console.ReadLine();
            if (bookAuthor == "b") run.bookInfoChangeMenu();
            if (exception.bookAuthorCheck(bookAuthor))
            {
                this.changeAuthorFunc();
            }
            sd.update("book", "Author", bookAuthor, "BookNo", bookNo);
            print.successModifyMessage();
        }

        // 책 수량 수정기능
        public void changeQuantityFunc()
        {
            print.enterBookForModify(3);
            bookQuantity = Console.ReadLine();
            if (bookQuantity == "b") run.bookInfoChangeMenu();
            if (exception.bookQuantityCheck(bookQuantity))
            {
                this.changeQuantityFunc();
            }
            sd.update("book", "Quantity", bookQuantity, "BookNo", bookNo);
            print.successModifyMessage();
        }
        
        // 책 가격 수정기능
        public void changePriceFunc()
        {
            print.enterBookForModify(4);
            bookPrice = Console.ReadLine();
            if (bookPrice == "b") run.bookInfoChangeMenu();
            if (exception.bookPriceCheck(bookPrice))
            {
                this.changePriceFunc();
            }
            else if (string.IsNullOrWhiteSpace(bookPrice))
            {
                bookPrice = "FREE";
            }
            sd.update("book", "Price", bookPrice, "BookNo", bookNo);
            print.successModifyMessage();
        }

        // 모두바꾸기
        public void chnageAllFunc()
        {
            changeNameFunc();
            changeAuthorFunc();
            changeQuantityFunc();
            changePriceFunc();
        }

        // 입력받은 param 값으로 책의 이름을 찾는다
        // 만약 그런 책이 존재하지 않다면 false를 반환
        public bool printBookNameFunc(string param)
        {
            bool chk = false;
            // 타이틀 출력
            print.bookTitle();

            // 중복체크 (책이 만약 없으면)
            //if (!sd.selectForExists("book", "Name", param)) { return false; }
                
            // DB에서 book 테이블 끌어옴
            ds = sd.selectAll("book");

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                if (Convert.ToString(r["Name"]).Contains(param))
                {
                    chk = true;

                    // 데이터 삽입후
                    sd.BookData.BookNo = Convert.ToString(r["BookNo"]);
                    sd.BookData.BookName = Convert.ToString(r["Name"]);
                    sd.BookData.BookAuthor = Convert.ToString(r["Author"]);
                    sd.BookData.BookPrice = Convert.ToString(r["Price"]);
                    sd.BookData.BookQuantity = Convert.ToString(r["Quantity"]);

                    // 출력
                    print.bookElement(sd.BookData);
                    print.bookEndLine();
                }
            }
            // 책이 아무것도 없다면 False 반환
            if (chk) { return true; }
            else { return false; }
        }

        // DB에 데이터가 하나도 없을경우
        public void noExistBookFunc()
        {
            if (sd.countRow("book") == 0)
            {
                print.noExistDeleteBook();
                run.bookMenu();
            }
        } // Method - noExistBookFunc

        ///////////////////////////////////////////////////////// 도서대여
        public void rentBook()
        {
            // 책이 하나도 없을경우
            noExistBookFunc();

            // ID 로그인 부분
            // ID 가 존재하면 idCheckFunc 에서 TRUE를 반환함
            if (idCheckFunc()) { print.enterRentBookName(); }
            else { rentBook(); }

            // 책 이름 입력받기
            bookName = Console.ReadLine();
            if (bookName == "b") run.start();
            // 만약 책이 존재하지 않는다면
            if(!printBookNameFunc(bookName))
            {
                print.noExistBook();
                run.start();
            }
            // 책의 번호를 골라야할 차례
            selectNoForRentBookFunc();
        }

        // 책 대여를 위한 로그인
        public bool idCheckFunc()
        {
            // 아이디 입력부분
            print.loginIdMessage();
            id = Console.ReadLine();
            if (id == "b") run.start();

            // ID가 존재한다면 패스워드 입력받음
            // 존재하지 않으면 FALSE 리턴
            if (sd.selectForExists("member", "ID", id))
            {
                pwCheckFunc(id);
            }
            else
            {
                print.noExistIdMessage();
                return false;
            }
            return true;
        }

        // 아이디 입력이 성공하면 패스워드를 입력받는데 
        // 위에서 입력한 ID를 받아온다
        public void pwCheckFunc(string Id)
        {
            string PW;

            // 패스워드를 입력받고
            print.loginPwMessage();
            PW = showStarPW();
            if (PW == "b") { run.start(); }

            // 해당 ID에 해당하는 데이터를 DB에서 가져옴
            ds = sd.selectCondition("member", "ID", Id);

            // 해당 ID에 대한 비밀번호가 맞다면 TRUE 리턴
            // 아니라면 에러메세지 출력 후 함수 재호출
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                if (PW == Convert.ToString(r["PW"])) { return; }
                else
                {
                    print.noMatchPW();
                    pwCheckFunc(Id);
                }
            }
        }

        // 책 대여를 위한 번호선택 메소드
        public void selectNoForRentBookFunc()
        {
            // 책의 번호를 입력받는다
            print.enterBookNoForRent();
            bookNo = Console.ReadLine();
            if (bookNo == "b") run.start(); // Back

            // 책의 번호가 DB에 존재하면
            // 해당 책의 번호에 정보를 업데이트 시킨다. (= 대여)
            // 예외처리 : 이미 책이 대여중이라면
            if (sd.selectForExists("book", "BookNo", bookNo))
            {
                ds = sd.selectCondition("rent", "Fno", bookNo);

                foreach(DataRow r in ds.Tables[0].Rows)
                {
                    // 대여중이 아니라면 대여하고 완료메세지
                    if (Convert.ToString(r["RentCheck"]) == "0")
                    {
                        sd.update("rent", "BookRentID", id, "Fno", bookNo);
                        sd.update("rent", "BookRentTime", DateTime.Now.ToString(), "Fno", bookNo);
                        sd.update("rent", "RentCheck", "1", "Fno", bookNo);
                        print.completeRentMessage();
                        borrowLog(bookNo);
                    }
                    // 이미 대여중이라면
                    else
                    {
                        print.alreadyRentBook();
                        selectNoForRentBookFunc();
                    }
                }
            }
            // 책의 번호가 DB에 존재하지 않으면
            else
            {
                print.ErrorMessage();
                selectNoForRentBookFunc();
            }
            run.bookMenu();
        }

        ///////////////////////////////////////////////////////// 도서반납
        public void returnBook()
        {
            bool chk = false;

            // 반납을 위해 책 이름을 검색 후 입력받음
            print.enterReturnBookName();
            bookName = Console.ReadLine();
            if (bookName == "b") run.start();

            // 존재한다면 대여된 것들만 출력
            printAlreadyRentBook(bookName);
            // 반납받을 책의 번호를 입력하고
            print.enterRentBookNo();
            bookNo = Console.ReadLine();
            if (bookNo == "b") run.start();

            // 해당 번호의 책을 반납한다
            ds = sd.selectCondition("rent", "RentCheck", "1");

            // 예외처리 : 해당 번호가 있는지 없는지 체크하고, 대여중인지 아닌지 체크
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                if ((Convert.ToString(r["Fno"]) == bookNo) && (Convert.ToString(r["RentCheck"]) == "1"))
                {
                    returnLog(bookNo);
                    chk = true;
                    sd.update("rent", "bookRentID", "", "Fno", bookNo);
                    sd.update("rent", "bookRentTime", "", "Fno", bookNo);
                    sd.update("rent", "RentCheck", "0", "Fno", bookNo);
                    print.returnBookSuccess();
                    run.start();
                }
            }

            // 만약 데이터가 맞지 않는다면
            if (!chk)
            {
                print.ErrorMessage();
                returnBook();
            }
        }

        // 책이 존재하는지 체크
        public void printAlreadyRentBook(string bookName)
        {
            List<string> bookNo = new List<string>();

            
            // 만약 대여된 책이 있다면 (TRUE를 받아옴)
            if (sd.selectForExists("rent", "RentCheck", "1"))
            {
                // rent 테이블에서 대여된 책의 번호를 받아오고
                ds = sd.selectCondition("rent", "RentCheck", "1");
                foreach(DataRow r in ds.Tables[0].Rows)
                {
                    bookNo.Add(Convert.ToString(r["Fno"]));
                }

                // 그 번호의 갯수만큼 반복문을 돌려서 출력해준다
                for (int i = 0; i < bookNo.Count; i++)
                {
                    ds = sd.selectCondition("book", "BookNo", bookNo[i]);

                    print.bookTitle();
                    foreach(DataRow r in ds.Tables[0].Rows)
                    {
                        sd.BookData.BookNo = bookNo[i];
                        sd.BookData.BookName = Convert.ToString(r["Name"]);
                        sd.BookData.BookAuthor = Convert.ToString(r["Author"]);
                        sd.BookData.BookPrice = Convert.ToString(r["Price"]);
                        sd.BookData.BookQuantity = Convert.ToString(r["Quantity"]);

                        print.bookElement(sd.BookData);
                        print.bookEndLine();
                    }
                }
            }
            // 대여된 책이 없다면
            else
            {
                print.noExistBook();
                run.start();
            }
        }

        // 비밀번호를 * 로 표시해주는 메소드
        public string showStarPW()
        {
            ConsoleKeyInfo key;

            string pass = "";
            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Substring(0, (pass.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            }
            while (key.Key != ConsoleKey.Enter);

            return pass;
        } // method - password

        public void borrowLog(string bookNo)
        {
            string time = DateTime.Now.ToString();
            ds = sd.selectAll("book");

            foreach(DataRow r in ds.Tables[0].Rows)
            {
                if (bookNo == Convert.ToString(r["BookNo"]))
                {
                    string log = "책 대여 : " + id + " 님이 " + Convert.ToString(r["Name"]) + " (을/를) " + time + " 에 빌리셨습니다.";
                    sd.insertLog(log);
                }
            }
        } // Method - borrowLog

        public void returnLog(string bookNo)
        {
            string time = DateTime.Now.ToString();
            string id = "";

            ds = sd.selectAll("rent");
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                if (bookNo == Convert.ToString(r["Fno"]))
                {
                    id = Convert.ToString(r["BookRentID"]);
                    break;
                }
            }

            ds = sd.selectAll("book");

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                if (bookNo == Convert.ToString(r["BookNo"]))
                {
                    string log = "책 반납 : " + id + " 님이 " + Convert.ToString(r["Name"]) + " (을/를) " + time + " 에 반납하셨습니다.";
                    sd.insertLog(log);
                }
            }
        } // Method - borrowLog

    } 
}
