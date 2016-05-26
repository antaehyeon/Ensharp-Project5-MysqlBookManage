using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_BookStore
{
    class BookManagement
    {
        private Print print;
        private SharingData sd;
        private Exception exception;
        private Run run;

        private string bookName;
        private string bookAuthor;
        private string bookPrice;
        private string bookQuantity;
        private string bookRentTime;
        private string bookRentID;

        private string regBookQuantity;
        private int index = 0;

        public BookManagement(Run run)
        {
            print = new Print();
            this.run = run;
            exception = new Exception();
            sd = SharingData.GetInstance();
        }

        public void registerBookFunction(int mode)
        {
            switch(mode)
            {
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

            // bookNo = regBookQuantity (등록된 갯수)
            sd.RegisteredBookQuantity++;
            regBookQuantity = Convert.ToString(sd.RegisteredBookQuantity);

            BookVO bookData = new BookVO(regBookQuantity, bookName, bookAuthor, bookPrice, bookQuantity);
            sd.BookList.Add(bookData);
            print.bookRegisterSuccessMessage();
            run.bookMenu();
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
            this.noExistBookFunc();
            print.findBookName();
            bookName = Console.ReadLine();
            if (bookName == "b") run.bookMenu();
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
            string strNo;
            int iNo;

            this.noExistBookFunc();
            print.deleteBookTitle();
            this.printAllBookFunc();
            print.enterBookNumberForDelete();
            strNo = Console.ReadLine();
            if (strNo == "b") run.bookMenu();
            if (exception.bookNoCheck(strNo))
            {
                this.deleteBookFunction();
            }
            iNo = Convert.ToInt32(strNo);
            if (iNo > 0 && iNo <= sd.RegisteredBookQuantity)
            {
                index = this.findBookIndex(strNo, 1);
                sd.BookList.RemoveAt(index);
                print.bookDeleteSuccessMessage();
                sd.RegisteredBookQuantity--;
                this.deleteBookFunction();
            }
            else
            {
                print.ErrorMessage();
                this.deleteBookFunction();
            }
        }

        // 책 변경기능
        public void changeBookFunction()
        {
            index = 0;

            this.noExistBookFunc();
            print.findBookName();
            bookName = Console.ReadLine();
            if (bookName == "b") run.bookMenu();

            modifyBookSelect();
        }

        public void modifyBookSelect()
        {
            int iNo;
            string strNo;

            // 책이 나오고 select 부분
            print.modifyBookTitle();
            if (!printBookNameFunc(bookName))
            {
                print.noExistBook();
                changeBookFunction();
            }
            print.enterBookNumberForModify();

            strNo = Console.ReadLine();
            if (strNo == "b") run.bookMenu();
            if (exception.bookNoCheck(strNo))
            {
                this.modifyBookSelect();
            }
            iNo = Convert.ToInt32(strNo);
            if (iNo > 0 && iNo <= sd.RegisteredBookQuantity)
            {
                index = this.findBookIndex(strNo, 1);
                run.bookInfoChangeMenu();
            }
            else
            {
                print.ErrorMessage();
                this.modifyBookSelect();
            }
        }

        // 책 이름 수정기능
        public void changeNameFunc()
        {
            print.enterBookForModify(1);
            bookName = Console.ReadLine();
            if (bookName == "b") run.bookInfoChangeMenu();
            if(exception.bookNameCheck(bookName))
            {
                this.changeNameFunc();
            }
            sd.BookList[index].BookName = bookName;
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
            sd.BookList[index].BookAuthor = bookAuthor;
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
            sd.BookList[index].BookQuantity = bookQuantity;
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
            sd.BookList[index].BookPrice = bookPrice;
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
        
        // 책 위치찾기
        public int findBookIndex(string str, int mode)
        {
            for (int i = 0; i < sd.BookList.Count; i++)
            {
                if (mode == 1 && sd.BookList[i].BookNo == str)
                {
                    return i;
                }
                else if (mode == 2 && sd.BookList[i].BookName == str)
                {
                    return i;
                }
            }
            return -1;
        } // Method - findBookIndex

        public void printAllBookFunc()
        {
            print.bookTitle();
            for (int i = 0; i < sd.BookList.Count; i++)
            {
                print.bookElement(i);
                print.bookEndLine();
            }
        } // Method - printBookInfo

        // 입력받은 param 값으로 책의 이름을 찾는다
        // 만약 그런 책이 존재하지 않다면 false를 반환
        public bool printBookNameFunc(string param)
        {
            bool chk = false;
            print.bookTitle();
            for (int i = 0; i < sd.BookList.Count; i++)
            {
                if (sd.BookList[i].BookName.Contains(param))
                {
                    chk = true;
                    print.bookElement(i);
                    print.bookEndLine();
                }
            }
            if (chk == false)
            {
                return false;
            }
            return true;
        }

        public void noExistBookFunc()
        {
            if (sd.RegisteredBookQuantity == 0)
            {
                print.noExistDeleteBook();
                run.bookMenu();
            }
        } // Method - noExistBookFunc




        ///////////////////////////////////////////////////////// 도서대여

        public void rentBook()
        {
            if (sd.RegisteredBookQuantity == 0)
            {
                print.noExistBook();
                run.start();
            }

            int memberIndex = idCheckFunc();
            if (memberIndex == -1)
            {
                rentBook();
            }

            print.enterRentBookName();
            bookName = Console.ReadLine();
            if (bookName == "b") run.start(); // Back
            if(!printBookNameFunc(bookName)) // 만약 책이 존재하지 않는다면
            {
                print.noExistBook();
                run.start();
            }
            selectNoForRentBookFunc(memberIndex);
        }

        public void selectNoForRentBookFunc(int memberIndex)
        {
            int iNo;
            string strNo;

            // 책이 존재한다면 번호를 골라야할 차례
            print.enterBookNoForRent();
            strNo = Console.ReadLine();
            if (strNo == "b") run.start(); // Back
            if (exception.bookNoCheck(strNo)) // 예외처리(숫자만 받을수 있도록)
            {
                selectNoForRentBookFunc(memberIndex); // 재귀
            }
            iNo = Convert.ToInt32(strNo);
            if (iNo > 0 && iNo <= sd.RegisteredBookQuantity)
            {
                index = findBookIndex(strNo, 1);
                if (sd.BookList[index].BookRentTime != "")
                {
                    print.alreadyRentBook();
                    rentBook();
                }
                sd.BookList[index].BookRentTime = DateTime.Now.ToString();
                sd.BookList[index].BookRentID = sd.MemberList[memberIndex].MemberID;
                print.completeRentMessage();
            }
            else
            {
                print.ErrorMessage();
                selectNoForRentBookFunc(memberIndex);
            }
            run.bookMenu();
        }

        public int idCheckFunc()
        {
            string ID;

            print.loginIdMessage();
            ID = Console.ReadLine();
            if (ID == "b") run.start();
            for (int i = 0; i < sd.MemberList.Count; i++)
            {
                if (ID == sd.MemberList[i].MemberID && pwCheckFunc(i))
                {
                    return i;
                }
            }
            print.noExistIdMessage();
            return -1;
        }

        public bool pwCheckFunc(int index)
        {
            string PW;

            print.loginPwMessage();
            PW = showStarPW();
            if (PW == "b") run.start();

            if (sd.MemberList[index].PW == PW)
            {
                return true;
            }
            else
            {
                print.noMatchPW();
                pwCheckFunc(index);
            }
            return true;
        }

        ///////////////////////////////////////////////////////// 도서반납

        public void returnBook()
        {
            string bookNo;
            int index;

            print.enterReturnBookName();
            bookName = Console.ReadLine();
            if (bookName == "b") run.start();

            // 존재한다면 대여된 것들만 출력

            printAlreadyRentBook(bookName);
            print.enterRentBookNo();
            bookNo = Console.ReadLine();
            if (bookNo == "b") run.start();

            index = findBookIndex(bookNo, 1);

            sd.BookList[index].BookRentID = "";
            sd.BookList[index].BookRentTime = "";

            print.returnBookSuccess();
            run.start();
        }

        public void printAlreadyRentBook(string bookName)
        {
            bool chk = false;
            if (sd.RegisteredBookQuantity == 0)
            {
                print.noExistBook();
                run.start();
            }
            for (int i = 0; i < sd.BookList.Count; i++)
            {
                if (sd.BookList[i].BookRentID != "") { chk = true; }
            }
            if (chk== false)
            {
                print.noExistReturnBook();
                run.start();
            }
            print.bookTitle();
            for (int i = 0; i < sd.BookList.Count; i++)
            {
                if (sd.BookList[i].BookName.Contains(bookName) && sd.BookList[i].BookRentID != "")
                {
                    print.bookElement(i);
                    print.bookEndLine();
                }
            }
        }


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



    } 
}
