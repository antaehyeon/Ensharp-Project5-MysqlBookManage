                                        using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data;


namespace Project2_BookStore
{
    class Print
    {
        Run run;
        SharingData sd;
        DataSet ds;

        public Print()
        {
            sd = SharingData.GetInstance();
        }
        public Print(Run run)
        {
            sd = SharingData.GetInstance();
            this.run = run;
        }


        // 화살표로 움직일 수 있게 해주는 메소드
        public int moveArrow(int pWidth, int pHeight, int menuNumber, int mode)
        {
            ConsoleKeyInfo cki;
            int width = pWidth, height = pHeight;

            while (true)
            {
                Console.Clear();
                switch (mode)
                {
                    case 1: // 첫화면
                        firstMenu();
                        break;
                    case 2: // 멤버관리메뉴
                        memberMenu();
                        break;
                    case 3: // 수정메뉴
                        selectModifyItem();
                        break;
                    case 4: // 검색메뉴
                        selectSearchItem();
                        break;
                    case 5: // 도서메뉴
                        bookMenu();
                        break;
                    case 6: // 책 변경메뉴
                        modifyBookMenu();
                        break;
                }

                Console.SetCursorPosition(width, height);
                Console.Write('→');

                // KEY 의 입력을 받는 부분
                cki = Console.ReadKey(true);
                switch (cki.Key)
                {
                    case ConsoleKey.UpArrow:
                        height--;
                        break;
                    case ConsoleKey.DownArrow:
                        height++;
                        break;
                    case ConsoleKey.Enter:
                        return height;
                }

                // 맨위에서 UpArrow 이벤트가 발생했을 때(즉 위방향키를 눌렀을 때) 맨 아래로 가게해주도록 설계
                if (height == pHeight - 1)
                {
                    height = pHeight + menuNumber - 1;
                }
                else if (height == pHeight + menuNumber)
                {
                    height = pHeight;
                }
            }
        }
        // 처음 메뉴 출력
        public void firstMenu()
        {
            // Console 창의 크기를 조절
            Console.SetWindowSize(124, 40);
            Console.Clear();
            title("도서관리 프로그램");

            Console.Write(hangleCenterArrange(124, "[  ] 회원 관리"));
            Console.Write(hangleCenterArrange(124, "[  ] 도서 관리"));
            Console.Write(hangleCenterArrange(124, "[  ] 도서 대여"));
            Console.Write(hangleCenterArrange(124, "[  ] 도서 반납"));
            Console.Write(hangleCenterArrange(124, "[  ] 기록 확인"));
            Console.Write(hangleCenterArrange(124, "[  ] 종     료"));
        } // method - printFirstMenu

        // 회원쪽 메뉴 출력
        public void memberMenu()
        {
            // Console 창의 크기를 조절
            Console.SetWindowSize(124, 40);
            Console.Clear();
            title("회원 관리");

            Console.Write(hangleCenterArrange(124, "[  ] 회원 등록"));
            Console.Write(hangleCenterArrange(124, "[  ] 회원 수정"));
            Console.Write(hangleCenterArrange(124, "[  ] 회원 삭제"));
            Console.Write(hangleCenterArrange(124, "[  ] 회원 검색"));
            Console.Write(hangleCenterArrange(124, "[  ] 회원 출력"));
            Console.Write(hangleCenterArrange(124, "[  ] 뒤로 가기"));
        } // method - printFirstMenu

        // 수정 메뉴 출력
        public void selectModifyItem()
        {
            Console.SetWindowSize(124, 40);
            Console.Clear();
            title("수정 메뉴");

            Console.Write(hangleCenterArrange(124, "[  ] 이      름"));
            Console.Write(hangleCenterArrange(124, "[  ] 핸드폰번호"));
            Console.Write(hangleCenterArrange(124, "[  ] 패스  워드"));
            Console.Write(hangleCenterArrange(124, "[  ] 뒤로  가기"));
        }

        // 검색 메뉴 출력
        public void selectSearchItem()
        {
            Console.SetWindowSize(124, 40);
            Console.Clear();
            title("검색 메뉴");
            Console.Write(hangleCenterArrange(124, "[  ] 아이디로 검색"));
            Console.Write(hangleCenterArrange(124, "[  ] 이름으로 검색"));
            Console.Write(hangleCenterArrange(124, "[  ] 뒤 로   가 기"));
        }

        // 도서 메뉴 출력
        public void bookMenu()
        {
            Console.SetWindowSize(124, 40);
            Console.Clear();
            title("수행할 작업을 선택하세요");

            Console.Write(hangleCenterArrange(124, "[  ] 도서 등록"));
            Console.Write(hangleCenterArrange(124, "[  ] 도서 찾기"));
            Console.Write(hangleCenterArrange(124, "[  ] 도서 출력"));
            Console.Write(hangleCenterArrange(124, "[  ] 도서 삭제"));
            Console.Write(hangleCenterArrange(124, "[  ] 도서 변경"));
            Console.Write(hangleCenterArrange(124, "[  ] 뒤로 가기"));
        }

        public void modifyBookMenu()
        {
            Console.SetWindowSize(124, 40);
            Console.Clear();
            title("수정할 항목을 선택하세요");

            Console.Write(hangleCenterArrange(124, "[  ] 도서 이름"));
            Console.Write(hangleCenterArrange(124, "[  ] 도서 저자"));
            Console.Write(hangleCenterArrange(124, "[  ] 도서 수량"));
            Console.Write(hangleCenterArrange(124, "[  ] 도서 가격"));
            Console.Write(hangleCenterArrange(124, "[  ] 전체 변경"));
            Console.Write(hangleCenterArrange(124, "[  ] 뒤로 가기"));
        }

        // ID 관련 출력
        public void enterIdMessage()
        {
            Console.Clear();
            title("ID 입력");
            Console.WriteLine("\n 아이디는 8자 ~ 16자까지 가능합니다");
            Console.WriteLine(" 아이디는 영어와 숫자로만 구성이 가능합니다");
            Console.WriteLine(" 아이디는 중복될 수 없습니다");
            Console.WriteLine(" 등록하실 ID를 입력하세요 (뒤로가시려면 b 를 입력하세요)");
            Console.Write(" → ");
        }

        public void lengthNotSatisfyMessage()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("ID가 너무 짧습니다");
            Console.ReadKey();
        }

        public void idFirstLetterNoNumMessage()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("ID의 첫 문자는 숫자로 시작할 수 없습니다");
            Console.ReadKey();
        }

        public void lengthOverMessage()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("ID는 14자를 초과할 수 없습니다");
            Console.ReadKey();
        }

        public void idIsNullMessage()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("ID는 NULL 이거나 공백일 수 없습니다");
            Console.ReadKey();
        }

        public void onlyEnglishAndNumMessage()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("ID는 영어와 숫자로만 구성이 가능합니다");
            Console.ReadKey();
        }

        public void duplicationIdMessage()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("중복된 ID는 등록하실 수 없습니다");
            Console.ReadKey();
        }

        public void notFindIdMessage()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("ID를 찾을 수 없습니다");
            Console.ReadKey();
        }

        // PW 관련 출력
        public void enterPwMessage()
        {
            Console.Clear();
            title("패스워드 설정");
            Console.WriteLine("\n 패스워드를 입력하세요 (뒤로 가시려면 b를 눌러주세요)");
            Console.Write(" → ");
        }

        public void disaccordPw()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("먼저 입력한 패스워드와 일치하지 않습니다");
            Console.ReadKey();
        }

        public void pwIsNullMessage()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("패스워드는 NULL 이거나 공백일 수 없습니다");
            Console.ReadKey();
        }

        public void checkPwMessage()
        {
            Console.WriteLine("\n 패스워드를 한번 더 입력하세요");
            Console.Write(" → ");
        }

        public void discordPwMessage()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("패스워드가 맞지 않습니다");
            Console.ReadKey();
        }

        // 이름 관련 출력
        public void enterName()
        {
            Console.Clear();
            title("이름 입력");
            Console.WriteLine("\n 이름은 2자부터 6자까지 한글로만 가능합니다");
            Console.WriteLine(" 이름을 입력하세요 (뒤로가시려면 b 를 입력하세요)");
            Console.Write(" → ");
        }

        public void nameErrorMessage()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("이름 제한조건을 다시 확인하세요");
            Console.ReadKey();
        }

        // 핸드폰 관련 출력
        public void enterPhoneNum()
        {
            Console.Clear();
            title("핸드폰번호 입력");
            Console.WriteLine("\n 핸드폰번호는 10-11자리만 가능합니다");
            Console.WriteLine(" 핸드폰번호를 입력하세요 (뒤로가시려면 b 를 입력하세요)");
            Console.Write(" → ");
        }

        public void phoneNumLengthOverMessage()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("핸드폰번호 자릿수를 지켜주세요");
            Console.ReadKey();
        }

        public void existsPhoneNumMessage()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("핸드폰번호가 중복됩니다");
            Console.ReadKey();
        }

        // 결과 출력
        public void idRegisterSccessMessage(string ID, string name, string phoneNum, string createTime)
        {
            Console.Clear();
            title("정상 등록");
            Console.WriteLine("\n");
            Console.WriteLine(" ID : {0}", ID);
            Console.WriteLine(" 이름 : {0}", name);
            Console.WriteLine(" 핸드폰번호 : {0}", phoneNum);
            Console.WriteLine("\n {0} 에 정상적으로 등록되었습니다", createTime);
            Console.ReadKey();
        }

        public void searchIdResult(string id, string name, string phoneNum, string createTime)
        {
            Console.Clear();
            title("결과 출력");
            Console.WriteLine("{0} 로 찾으신 정보입니다\n\n", id);
            Console.WriteLine(" 이름 : {0}", name);
            Console.WriteLine(" 핸드폰 번호 : {0}", phoneNum);
            Console.WriteLine(" 생성 시간 : {0}", createTime);

            Console.WriteLine("\n\n전 메뉴로 가시려면 아무키나 누르세요");
            Console.ReadKey();
        }

        public void memberTitle()
        {
            memeberStartLine();
            Console.Write("┃{0}", hangleCenterArrange(16, "ID"));
            Console.Write("┃{0}", hangleCenterArrange(12, "이름"));
            Console.Write("┃{0}", hangleCenterArrange(12, "핸드폰번호"));
            Console.WriteLine("┃{0}┃", hangleCenterArrange(24, "생성 시간"));
            memberEndLine();
        }

        public void memberResult(string id, string name, string phoneNum, string createTime)
        {
            Console.Write("┃{0, -16}", id);
            Console.Write("┃{0, -9}", name);
            Console.Write("┃{0, -12}", phoneNum);
            Console.WriteLine("┃{0, -22}┃", createTime);
        }

        public void memberListTitle()
        {
            Console.Clear();
            title("회원 출력");
            Console.WriteLine(" 회원을 전체 출력합니다");
            memberTitle();
        }

        // 종료메뉴
        public void exitMessage()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n");
            title("도서관리 프로그램을 종료합니다");
            Environment.Exit(0);
        }

        // 수정관련
        public void modifyMessage()
        {
            Console.Clear();
            title("로그인 - ID");
            Console.WriteLine("\n");
            Console.WriteLine(" 수정을 하기위해 로그인이 필요합니다");
            Console.WriteLine(" 아이디를 입력하세요 (뒤로 가시려면 b 를 입력하세요)");
            Console.Write(" → ");
        }

        public void enterPwForModify()
        {
            Console.Clear();
            title("로그인 - 패스워드");
            Console.WriteLine("\n");
            Console.WriteLine("패스워드를 입력하세요 (뒤로 가시려면 b 를 입력하세요)");
            Console.Write(" → ");
        }

        public void modifyName()
        {
            Console.Clear();
            title("이름 수정");
            Console.WriteLine("수정할 이름을 입력하세요 (뒤로 가시려면 b 를 입력하세요)");
            Console.Write(" → ");
        }

        public void modifySuccessResult()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("정상적으로 수정되었습니다");
            Console.ReadLine();
        }

        public void enterPhoneNumForModify()
        {
            Console.Clear();
            title("핸드폰번호 수정");
            Console.WriteLine("수정할 번호를 입력하세요 (뒤로 가시려면 b 를 입력하세요)");
            Console.Write(" → ");
        }

        // 삭제 관련
        public void enterIdForDelete()
        {
            Console.Clear();
            title("삭제 메뉴");
            Console.WriteLine("\n 삭제할 아이디를 입력하세요 (뒤로 가시려면 b 를 입력하세요)");
            Console.Write(" → ");
        }

        public void enterPwForDelete()
        {
            Console.Clear();
            title("삭제 메뉴");
            Console.WriteLine("\n 비밀번호를 입력하세요 (뒤로 가시려면 b 를 입력하세요)");
            Console.Write(" → ");
        }

        public void deleteSuceessMessage()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("정상적으로 삭제되었습니다");
            Console.ReadKey();
        }

        // 검색 관련
        public void enterIdForSearch()
        {
            Console.Clear();
            title("아이디로 검색");
            Console.WriteLine("\n 검색할 아이디를 입력하세요 (뒤로 가시려면 b 를 입력하세요)");
            Console.Write(" → ");
        }

        public void enterNameForSearch()
        {
            Console.Clear();
            title("이름으로 검색");
            Console.WriteLine("\n 검색할 이름을 입력하세요 (뒤로 가시려면 b 를 입력하세요)");
            Console.Write(" → ");
        }


        // 도서 관련
        public void enterBookNoMessage()
        {
            Console.Clear();
            title("도서번호 입력");
            Console.WriteLine("\n 등록할 책의 고유번호를 입력하세요 (숫자 4자까지만 가능)");
            Console.WriteLine(" 뒤로 가시려면 b 를 입력하세요");
            Console.Write(" → ");
        }

        public void enterBookNameMessage()
        {
            Console.Clear();
            title("도서명 입력");
            Console.WriteLine("\n 등록할 책 이름을 입력하세요");
            Console.WriteLine(" 책 이름은 14자까지 가능합니다");
            Console.WriteLine(" 책 이름은 공백으로 시작할 수 없습니다");
            Console.WriteLine(" 책 이름의 특수문자는 !, ?, - 만 허용합니다");
            Console.WriteLine(" 뒤로 가시려면 b 를 입력하세요");
            Console.Write(" → ");
        }

        public void bookNoExistsMessage()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("책 고유번호는 중복될 수 없습니다");
            Console.ReadKey();
        }

        public void bookNameFirstNoSpaceMessage()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("책 이름은 공백으로 시작할 수 없습니다");
            Console.ReadKey();
        }

        public void bookNameLengthOver()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("책 이름이 너무 짧거나 너무 깁니다");
            Console.ReadKey();
        }

        public void bookNameWrongMessage()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("책 이름이 잘못되었습니다");
            Console.ReadKey();
        }

        // 책 저자 관련
        public void enterBookAuthorMessage()
        {
            Console.Clear();
            title("책 저자 입력");
            Console.WriteLine("\n 등록한 책의 저자를 입력하세요");
            Console.WriteLine(" 책 저자는 10자 까지 가능합니다");
            Console.WriteLine(" 책 저자는 공백으로 시작할 수 없습니다");
            Console.WriteLine(" 책 저자는 한글, 영어, 공백만 가능합니다");
            Console.WriteLine(" 뒤로 가시려면 b 를 입력하세요");
            Console.Write(" → ");
        }

        public void bookAuthorFirstNoSpaceMessage()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("책 저자이름은 공백으로 시작할 수 없습니다");
            Console.ReadKey();
        }

        public void bookAuthorErrorMessage()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("책 저자이름은 영어,한글,공백 이외의 문자는 불가능합니다");
            Console.ReadKey();
        }

        // 책 수량 관련
        public void enterBookQuantity()
        {
            Console.Clear();
            title("책 수량 입력");
            Console.WriteLine("\n 책의 수량은 1권부터 99권까지만 가능합니다");
            Console.WriteLine(" 책의 수량을 입력해주세요");
            Console.WriteLine(" 뒤로 가시려면 b 를 입력하세요");
            Console.Write(" → ");
        }

        public void bookQuantityOverMessage()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("책 수량은 1권부터 99권까지만 가능합니다");
            Console.ReadKey();
        }

        public void bookQuantityOnlyNumber()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("책 수량은 숫자만 입력가능하며, 2자리수를 넘을 수 없습니다");
            Console.ReadKey();
        }

        // 책 가격 부분
        public void enterBookPrice()
        {
            Console.Clear();
            title("책 가격 입력");
            Console.WriteLine("\n 책의 가격을 입력하세요");
            Console.WriteLine(" 책의 가격은 100만원까지만 가능합니다");
            Console.WriteLine(" 공백만 입력하거나 아무것도 입력하지 않으시면 FREE로 등록됩니다");
            Console.WriteLine(" 뒤로 가시려면 b 를 입력하세요");
            Console.Write(" → ");
        }

        public void bookPriceOverMessage()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("책 가격은 백만원을 넘을 수 없습니다");
            Console.ReadKey();
        }

        public void bookPriceOnlyNumber()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("책 가격은 숫자만 입력하실 수 있습니다");
            Console.ReadKey();
        }

        // 책 결과 출력
        public void bookRegisterSuccessMessage()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("정상적으로 등록되었습니다");
            Console.ReadKey();
        }

        public void ErrorMessage()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("ERROR !");
            Console.ReadKey();
        }

        // 책 찾는 부분
        public void findBookName()
        {
            Console.Clear();
            title("책 검색메뉴");
            Console.WriteLine("\n 책 이름으로만 검색하실 수 있습니다");
            Console.WriteLine(" 책 이름부분에 포함되어도 출력합니다");
            Console.WriteLine(" 공백일 경우 전부 출력합니다");
            Console.WriteLine(" 뒤로가시려면 b 를 입력하세요");
            Console.Write(" → ");
        }

        public void noExistBook()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("책이 존재하지 않습니다");
            Console.ReadKey();
        }

        // 책 출력 부분
        public void bookTitle()
        {
            bookStartLine();
            Console.Write("┃{0}", hangleCenterArrange(4, "No"));
            Console.Write("┃{0}", hangleCenterArrange(28, "도서명"));
            Console.Write("┃{0}", hangleCenterArrange(20, "도서 저자"));
            Console.Write("┃{0}", hangleCenterArrange(2, "수량"));
            Console.Write("┃{0}", hangleCenterArrange(10, "가격"));
            Console.Write("┃{0}", hangleCenterArrange(24, "대여 시간"));
            Console.WriteLine("┃{0}┃", hangleCenterArrange(16, "대여자"));
            bookEndLine();
        }

        // 책 대여 부분
        public void enterRentBookName()
        {
            Console.Clear();
            title("책 대여");
            Console.WriteLine("\n 대여를 하기 위해 책 이름으로 검색해주세요");
            Console.WriteLine(" 책 이름의 부분에 포함되도 전부 검색됩니다");
            Console.WriteLine(" 뒤로 가시려면 b 를 입력하세요");
            Console.Write(" → ");
        }

        public void enterBookNoForRent()
        {
            Console.WriteLine(" 대여하실 책의 번호를 입력하세요");
            Console.WriteLine(" 뒤로 가시려면 b 를 입력하세요");
            Console.Write(" → ");
        }
        
        public void completeRentMessage()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("대여가 완료되었습니다");
            Console.ReadKey();
        }

        public void loginIdMessage()
        {
            Console.Clear();
            title("로그인");
            Console.WriteLine(" 책을 대여하기 위해서는 로그인이 필요합니다");
            Console.WriteLine(" b를 입력하시면 이전메뉴로 이동합니다");
            Console.Write("\n ID : ");
        }

        public void loginPwMessage()
        {
            Console.Write("\n PW : ");
        }

        public void noExistIdMessage()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("아이디가 등록되어 있지 않습니다");
            Console.ReadKey();
        }

        public void noPrintInfoMessage()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("출력할 정보가 존재하지 않습니다");
            Console.ReadKey();
        }

        public void noMatchPW()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("비밀번호가 맞지 않습니다");
            Console.ReadKey();
        }

        public void alreadyRentBook()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("이미 대여된 책입니다");
            Console.ReadKey();
        }

        // 책 반납부분
        public void enterReturnBookName()
        {
            Console.Clear();
            title("책 반납");
            Console.WriteLine("\n 반납을 하기 위해 책 이름을 검색합니다");
            Console.WriteLine(" 책 이름의 부분에 포함되도 전부 검색됩니다");
            Console.WriteLine(" 뒤로 가시려면 b 를 입력하세요");
            Console.Write(" → ");
        }

        public void noExistReturnBook()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("반납할 책이 존재하지 않습니다");
            Console.ReadKey();
        }

        public void returnBookSuccess()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("책이 정상적으로 반납되었습니다");
            Console.ReadKey();
        }

        public void enterRentBookNo()
        {
            Console.WriteLine(" 반납하실 책의 번호를 입력하세요");
            Console.Write(" → ");
        }

        // 책 출력부분
        public void bookElement(BookVO b)
        {
            string strWon = "";
            string rentId = "";
            string rentTime = "";

            if (b.BookPrice == "FREE") { strWon = "FREE"; }
            else
            {
                int won = Convert.ToInt32(b.BookPrice);
                strWon = won.ToString("#,##0");
            }

            ds = sd.selectCondition("rent", "Fno", b.BookNo);

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                rentId = Convert.ToString(r["BookRentID"]);
                rentTime = Convert.ToString(r["BookRentTime"]);
            }

            Console.Write("┃{0}", hangleCenterArrange(4, b.BookNo));
            Console.Write("┃{0}", hangleCenterArrange(28, b.BookName));
            Console.Write("┃{0}", hangleCenterArrange(20, b.BookAuthor));
            Console.Write("┃{0}", hangleCenterArrange(4, b.BookQuantity));
            Console.Write("┃{0}", hangleCenterArrange(10, strWon));
            Console.Write("┃{0}", hangleCenterArrange(24, rentTime));
            Console.WriteLine("┃{0}┃", hangleCenterArrange(16, rentId));
        }

        public void knowEndMessage()
        {
            Console.WriteLine(" 아무키나 누르시면 메뉴로 이동합니다");
            Console.ReadKey();
        }

        // 도서 삭제관련
        public void deleteBookTitle()
        {
            Console.Clear();
            title("도서 삭제");
        }

        public void enterBookNumberForDelete()
        {
            Console.WriteLine(" 삭제할 책의 번호(No) 를 입력하세요");
            Console.WriteLine(" 뒤로가시려면 b 를 입력하세요");
            Console.Write(" → ");
        }

        public void noExistDeleteBook()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("책이 존재하지 않습니다");
            Console.ReadKey();
        }
        
        public void bookDeleteSuccessMessage()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("정상적으로 삭제되었습니다");
            Console.ReadKey();
        }

        // 도서 변경 관련
        public void modifyBookTitle()
        {
            Console.Clear();
            title("도서 수정");
        }

        public void enterBookNumberForModify()
        {
            Console.WriteLine("수정할 책의 번호(No)를 입력하세요");
            Console.WriteLine("뒤로 가시려면 b 를 입력하세요");
            Console.Write(" → ");
        }

        public void enterBookForModify(int mode)
        {
            Console.Clear();
            if (mode == 1) title("책 이름 수정하기");
            else if (mode == 2) title("책 저자 수정하기");
            else if (mode == 3) title("책 수량으로 수정하기");
            else title("책 가격으로 수정하기");

            Console.WriteLine("\n 무엇으로 수정하시겠습니까?");
            Console.WriteLine("뒤로 가시려면 b 를 입력하세요");
            Console.Write(" → ");
        }

        public void successModifyMessage()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            title("정상적으로 수정되었습니다");
            Console.ReadKey();
        }

        public void log()
        {
            Console.Clear();
            title("기록 출력");
            ds = sd.selectAll("log");

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                Console.WriteLine("{0}", Convert.ToString(r["log"]));
            }
            Console.WriteLine("계속하려면 아무키나 눌러주세요");
            Console.ReadKey();
            run.start();
        }

        // length (총길이), strData (문자열) 을 이용해서
        // 문자열 이외의 부분은 공백으로 정렬하는 메소드
        public string hangleLineUp(int length, string strData)
        {
            string strToPrint = strData;
            int gap = length - Encoding.Default.GetBytes(strToPrint).Length;

            return "".PadLeft(gap) + strToPrint;
        }

        // 한글을 가운데 정렬해주는 메소드
        // length 를 이용해서 길이를 계산한다
        public string hangleCenterArrange(int length, string strData)
        {
            string strToPrint = strData;
            int gap = length - Encoding.Default.GetBytes(strToPrint).Length;

            if (gap < 0)
                return strData;

            int frontGap = gap / 2;
            int rearGap = gap - frontGap;

            return "".PadRight(frontGap) + strToPrint + "".PadRight(rearGap);
        }

        // 콘솔창 맨위의 TITLE 을 출력해주는 메소드
        // 안의 문구는 자동으로 가운데정렬을 시켜준다
        public void title(string StrData)
        {
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
            Console.WriteLine("\n{0}\n", hangleCenterArrange(124, StrData));
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        }

        public void memeberStartLine()
        {
            Console.WriteLine("┏━━━━━━━━┳━━━━━━┳━━━━━━┳━━━━━━━━━━━━┓");
        }

        public void memberMiddleLine()
        {
            Console.WriteLine("┣━━━━━━━━╋━━━━━━╋━━━━━━╋━━━━━━━━━━━━┫");
        }

        public void memberEndLine()
        {
            Console.WriteLine("┗━━━━━━━━┻━━━━━━┻━━━━━━┻━━━━━━━━━━━━┛");
        }

        public void bookStartLine()
        {
            Console.WriteLine("┏━━┳━━━━━━━━━━━━━━┳━━━━━━━━━━┳━━┳━━━━━┳━━━━━━━━━━━━┳━━━━━━━━┓");
        }

        public void bookEndLine()
        {
            Console.WriteLine("┗━━┻━━━━━━━━━━━━━━┻━━━━━━━━━━┻━━┻━━━━━┻━━━━━━━━━━━━┻━━━━━━━━┛");
        }
    } // Class - PrintMenu
}
