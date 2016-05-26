using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_BookStore
{
    class Exception
    {
        Print print;
        SharingData sd;

        public Exception()
        {
            print = new Print();
            sd = SharingData.GetInstance();
        } 


        // param : 입력받은 숫자에 따라서 문자열의 길이를 제한한다
        // true : 조건충족
        // false : 문자열의 길이가 입력받은 숫자보다 큼
        public bool stringLength(string str, int param)
        {
            if (str.Length > param) return true;
            else                    return false;
        }

        // 아이디 입력받을때 예외처리
        // MODE 1 : (영어, 숫자 6-14자 제한)
        // MODE 2 : (한글만, 2-6자 제한)
        // MODE 3 : (숫자만, 10~11자 제한)
        public bool stringCheck(string str, int mode)
        {
            string sPattern = "";
            switch (mode)
            {
                case 1: // ID쪽 (영어,숫자, 6-14자 제한)
                    sPattern = "^[a-z0-9]{6,14}$";
                    break;
                case 2: // 이름쪽 (한글만, 2-6자 제한)
                    sPattern = "^[가-힣]{2,6}$";
                    break;
                case 3: // 핸드폰번호쪽 (숫자만, 10~11자 제한)
                    sPattern = "^[0-9]{10,11}$";
                    break;
                case 4: // 책 저자 (영어,한글,공백만, 2~20자 제한)
                    sPattern = "^[a-zA-Z가-힣' ']{2,10}$";
                    break;
                case 5: // 책 가격 (숫자만 가능) , 책 수량 (숫자만 가능)
                    sPattern = "^([1-9][0-9]*)$";
                    break;
                case 6: // 책 이름 (한글,영어,공백,숫자,몇가지 특문만 허용, 1-10자 제한)
                    sPattern = "^[a-zA-Z0-9가-힣' '!?-]{2,16}$";
                    break;
            }
        
            if (System.Text.RegularExpressions.Regex.IsMatch(str, sPattern)) return false;
            else return true;
        }

        // 문자열의 첫번째 문자 체크 (숫자라면 true, 아니라면 false)
        public bool stringFirstLetterNumCheck(string str)
        {
            byte[] strToASCII = Encoding.ASCII.GetBytes(str);
            if (strToASCII[0] >= 48 && strToASCII[0] <= 57)
            {
                return true;
            }
            return false;
        }

        // 문자열의 첫번째 문자가 공백이면 TRUE
        public bool stringFirstLetterSpaceCheck(string str)
        {
            byte[] strToASCII = Encoding.ASCII.GetBytes(str);
            if (strToASCII[0] == 32)
            {
                return true;
            }
            return false;
        }

        // ID CHECK
        public bool idCheck(string ID)
        {
            if (string.IsNullOrWhiteSpace(ID)) // 문자열이 공백이거나 NULL 일경우
            {
                print.idIsNullMessage(); // ERROR
                return true;
            }
            if (ID.Length < 8) // ID가 너무 짧을경우
            {
                print.lengthNotSatisfyMessage(); // ERROR
                return true;
            }
            if (stringFirstLetterNumCheck(ID)) // ID 첫문자가 숫자일경우
            {
                print.idFirstLetterNoNumMessage(); // ERROR
                return true;
            }
            if (stringLength(ID, 14)) // 입력받은 문자의 길이가 14를 넘는조건
            {
                print.lengthOverMessage(); // ERROR
                return true;
            }
            if (stringCheck(ID, 1)) // 영어와 숫자만 들어가있는지 판별
            {
                print.onlyEnglishAndNumMessage(); // ERROR
                return true;
            }
            for (int i = 0; i < sd.MemberList.Count; i++) // 등록되있는 Member List 에서
            {
                if (sd.MemberList[i].MemberID == ID) // ID가 중복되는지 검사
                {
                    print.duplicationIdMessage(); // ERROR
                    return true;
                }
            }
            return false;
        }

        // PW CHECK
        public bool pwCheck(string PW, string tempPW)
        {
            if (PW != tempPW) // 입력한 두개의 패스워드가 일치하지 않을 때
            {
                print.disaccordPw(); // ERROR
                return true;
            }
            if (string.IsNullOrWhiteSpace(PW)) // 패스워드가 NULL이거나 공백만 있을경우
            {
                print.pwIsNullMessage(); // ERROR
                return true;
            }
            return false;
        }

        // PhoneNum Check
        public bool phoneNumCheck(string phoneNum)
        {
            if (stringCheck(phoneNum, 3))
            {
                print.phoneNumLengthOverMessage();
                return true;
            }
            for (int i = 0; i < sd.MemberList.Count; i++)
            {
                if (phoneNum == sd.MemberList[i].PhoneNum)
                {
                    print.existsPhoneNumMessage();
                    return true;
                }
            }
            return false;
        }

        // BookName Check
        public bool bookNameCheck(string bookName)
        {
            if (string.IsNullOrWhiteSpace(bookName) || stringFirstLetterSpaceCheck(bookName))
            {
                print.bookNameFirstNoSpaceMessage();
                return true;
            }
            if (stringCheck(bookName, 6))
            {
                print.bookNameWrongMessage();
                return true;
            }
            return false;
        }

        // BookAuthor Check
        public bool bookAuthorCheck(string bookAuthor)
        {
            if (string.IsNullOrWhiteSpace(bookAuthor) || stringFirstLetterSpaceCheck(bookAuthor))
            {
                print.bookAuthorFirstNoSpaceMessage();
                return true;
            }
            if (stringCheck(bookAuthor, 4))
            {
                print.bookAuthorErrorMessage();
                return true;
            }
            return false;
        }

        // BookQuantity Check
        public bool bookQuantityCheck(string bookQuantity)
        {
            if (string.IsNullOrWhiteSpace(bookQuantity))
            {
                print.bookQuantityOverMessage();
                return true;
            }
            if (stringCheck(bookQuantity, 5))
            {
                print.ErrorMessage();
                return true;
            }
            if (bookQuantity.Length == 0 || bookQuantity.Length >= 3)
            {
                print.ErrorMessage();
                return true;
            }
            return false;
        }

        // BookPrice Check
        public bool bookPriceCheck(string bookPrice)
        {
            if (string.IsNullOrWhiteSpace(bookPrice))
            {
                return false;
            }
            if (stringCheck(bookPrice, 5))
            {
                print.bookPriceOnlyNumber();
                return true;
            }
            if (Convert.ToInt32(bookPrice) > 1000000)
            {
                print.bookPriceOverMessage();
                return true;
            }
            return false;
        }

        // BookNo Check
        public bool bookNoCheck(string no)
        {
            if (stringCheck(no, 5))
            {
                print.ErrorMessage();
                return true;
            }
            return false;
        }

    } // Class - Exception
}
