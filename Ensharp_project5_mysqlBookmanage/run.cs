using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_BookStore
{
    class Run
    {
        Print print;
        MemberManagement member;
        BookManagement book;


        public Run()
        {
            print = new Print();
            member = new MemberManagement(this);
            book = new BookManagement(this);
        }

        // 처음 프로그램 시작 메뉴
        public void start()
        {
            Console.SetWindowSize(124, 40);
            int selectMenu = print.moveArrow(56, 8, 5, 1);

            switch (selectMenu)
            {
                case 8: // 회원등록
                    startMember();
                    break;
                case 9: // 도서관리
                    bookMenu();
                    break;
                case 10: // 도서대여
                    book.rentBook();
                    break;
                case 11: // 도서반납
                    book.returnBook();
                    break;
                case 12: // 종료
                    print.exitMessage();
                    break;
            } // switch
        }

        // 멤버관리 메뉴
        public void startMember()
        {
            while (true)
            {
                switch (print.moveArrow(56, 8, 6, 2))
                {
                    case 8: // 회원등록
                        member.registerID(1);
                        break;
                    case 9: // 회원수정
                        member.modifyMember();
                        break;
                    case 10: // 회원삭제
                        member.deleteMember();
                        break;
                    case 11: // 회원검색
                        searchMenu();
                        break;
                    case 12: // 회원출력
                        member.printMember();
                        break;
                    case 13: // 뒤로가기
                        start();
                        break;
                }
            }
        } // Method - startMember

        // 수정메뉴
        public void modifyMenu()
        {
            switch (print.moveArrow(54, 8, 4, 3))
            {
                case 8: // 이름
                    member.modifyName();
                    break;
                case 9: // 핸드폰번호
                    member.modifyPhoneNum();
                    break;
                case 10: // 비밀번호
                    member.modifyPassword();
                    break;
                case 11: // 뒤로가기
                    startMember();
                    break;
            }
        } // Method - modifyMenu

        // 검색메뉴
        public void searchMenu()
        {
            switch (print.moveArrow(54, 8, 3, 4))
            {
                case 8: // 아이디 검색
                    member.searchIdFunction();
                    break;
                case 9: // 이름 검색
                    member.searchNameFunction();
                    break;
                case 10: // 뒤로가기
                    startMember();
                    break;
            }
        } // Method - searchMenu

        // 책 관리메뉴
        public void bookMenu()
        {
            switch (print.moveArrow(56, 8, 6, 5))
            {
                case 8: // 도서등록
                    book.registerBookFunction(1);
                    break;
                case 9: // 도서찾기
                    book.findBookFunction();
                    break;
                case 10: // 도서출력
                    book.printBookFunction();
                    break;
                case 11: // 도서삭제
                    book.deleteBookFunction();
                    break;
                case 12: // 도서변경
                    book.changeBookFunction();
                    break;
                case 13: // 뒤로가기
                    start();
                    break;
            }
        } // Method - bookMenu

        public void bookInfoChangeMenu()
        {
            switch (print.moveArrow(56, 8, 6, 6))
            {
                case 8: // 책 이름수정
                    book.changeNameFunc();
                    bookMenu();
                    break;
                case 9: // 책 저자수정
                    book.changeAuthorFunc();
                    bookMenu();
                    break;
                case 10: // 책 수량변경
                    book.changeQuantityFunc();
                    bookMenu();
                    break;
                case 11: // 책 가격변경
                    book.changePriceFunc();
                    bookMenu();
                    break;
                case 12: // 전체
                    book.chnageAllFunc();
                    bookMenu();
                    break;
                case 13: // 뒤로가기
                    book.modifyBookSelect();
                    break;
            }
        } // Method - bookInfoChangeMenu
    }
}
