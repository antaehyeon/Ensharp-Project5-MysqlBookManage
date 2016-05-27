using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_BookStore
{
    class MemberManagement
    {
        private Print print;
        private Exception exception;
        private SharingData sd;
        private Run run;

        private string ID;
        private string name;
        private string phoneNum;
        private string PW;

        private int index;

        public MemberManagement(Run run)
        {
            exception = new Exception();
            print = new Print();
            sd = SharingData.GetInstance();
            this.run = run;
        } 

        public void registerID(int mode)
        {
            switch (mode)
            {
                case 1: // ID
                    ID = enterIdFunction();
                    registerID(2);
                    break;
                case 2: // PW
                    PW = enterPwFunction();
                    registerID(3);
                    break;
                case 3: // Name
                    name = enterNameFunction();
                    registerID(4);
                    break;
                case 4: // PhoneNumber
                    phoneNum = enterPhoneNumFunction();
                    break;
            }
            string creatTime = DateTime.Now.ToString();

            MemberVO data = new MemberVO(ID, name, creatTime, PW, phoneNum);
            sd.MemberList.Add(data);
            sd.memberInfoInsert(ID, name, phoneNum, PW, creatTime);
            print.idRegisterSccessMessage();

            run.startMember();
        }

        // ID 입력받는 기능
        public string enterIdFunction()
        {
            print.enterIdMessage();
            ID = Console.ReadLine();
            if (ID == "b") run.startMember(); // 뒤로가기
            if (exception.idCheck(ID))
            {
                enterIdFunction();
            }
            return ID;
        }

        // 비밀번호 입력받는 기능
        public string enterPwFunction()
        {
            string tempPW;
            print.enterPwMessage();
            PW = showStarPW();
            if (PW == "b") run.startMember();
            print.checkPwMessage();
            tempPW = showStarPW();
            if (tempPW == "b") run.startMember();

            if(exception.pwCheck(PW, tempPW))
            {
                enterPwFunction();
            }
            return PW;
        }
        
        // 이름 입력받는 기능
        public string enterNameFunction()
        {
            print.enterName();
            name = Console.ReadLine();
            if (name == "b") run.startMember();
            if (exception.stringCheck(name, 2))
            {
                print.nameErrorMessage();
                enterNameFunction();
            }
            return name;
        }

        // 핸드폰 번호 입력받는 기능
        public string enterPhoneNumFunction()
        {
            print.enterPhoneNum();
            phoneNum = Console.ReadLine();
            if (phoneNum == "b") run.startMember();
            if (exception.phoneNumCheck(phoneNum))
            {
                enterPhoneNumFunction();
            }
            return phoneNum;
        }

        // 회원수정
        public void modifyMember()
        {
            Console.Clear();
            print.modifyMessage();
            ID = Console.ReadLine();
            if (ID == "b") run.startMember();
            for (int index = 0; index < sd.MemberList.Count; index++)
            {
                if(ID == sd.MemberList[index].MemberID)
                {
                    print.enterPwForModify();
                    PW = showStarPW();
                    if (PW == "b") run.startMember();
                    if (PW == sd.MemberList[index].PW)
                    {
                        run.modifyMenu();
                        return;
                    }
                    else
                    {
                        print.discordPwMessage();
                        index--;
                        continue;
                    }
                }
            }
            print.notFindIdMessage();
            modifyMember();
        }

        // 이름수정
        public void modifyName()
        {
            print.modifyName();
            name = Console.ReadLine();
            if (name == "b") modifyMember();
            if (exception.stringCheck(name, 2))
            {
                modifyName();
            }
            sd.MemberList[index].MemberName = name;
            sd.modifyMember("Name", name, ID);
            print.modifySuccessResult();
        }

        // 핸드폰번호 수정
        public void modifyPhoneNum()
        {
            print.enterPhoneNumForModify();
            phoneNum = Console.ReadLine();
            if(exception.phoneNumCheck(phoneNum))
            {
                modifyPhoneNum();
            }
            sd.MemberList[index].PhoneNum = phoneNum;
            sd.modifyMember("PhoneNumber", phoneNum, ID);
            print.modifySuccessResult();
        }

        // 비밀번호 수정
        public void modifyPassword()
        {
            print.enterPwForModify();
            PW = showStarPW();
            if (exception.pwCheck(PW, PW))
            {
                modifyPassword();
            }
            sd.MemberList[index].PW = PW;
            sd.modifyMember("PW", PW, ID);
            print.modifySuccessResult();
        }

        // 회원삭제
        public void deleteMember()
        {
            print.enterIdForDelete();
            ID = Console.ReadLine();
            if (ID == "b") run.startMember();
            index = findIndex(ID, 1);
            if (index == -1) // MemberList에 ID가 없을경우
            {
                print.notFindIdMessage();
                deleteMember();
            }
            while(true)
            {
                print.enterPwForDelete();
                PW = showStarPW();
                if (PW == "b") run.startMember();
                if (sd.MemberList[index].PW == PW)
                {
                    sd.deleteMember(ID);
                    print.deleteSuceessMessage();
                    run.startMember();
                }
                else
                {
                    print.discordPwMessage();
                    continue;
                }
            }
        }

        // 회원검색 - 아이디
        public void searchIdFunction()
        {
            print.enterIdForSearch();
            ID = Console.ReadLine();
            if (ID == "b") run.searchMenu();
            index = findIndex(ID, 1);
            if (index == -1)
            {
                print.notFindIdMessage();
                searchIdFunction();
            }
            print.searchIdResult(index);
            run.searchMenu();
        }

        // 회원검색 - 이름
        public void searchNameFunction()
        {
            sd.SelectData();
            print.enterNameForSearch();
            name = Console.ReadLine();
            if (name == "b") run.searchMenu();

            print.memberTitle();
            for (int i = 0; i < sd.MemberList.Count; i++)
            {
                if(sd.MemberList[i].MemberName == name)
                {
                    print.memberResult(i);
                    print.memberEndLine();
                }
            }
            Console.ReadKey();
        }

        // 회원목록출력
        public void printMember()
        {
            print.memberListTitle();
            for (int i = 0; i < sd.MemberList.Count; i++)
            {
                print.memberResult(i);
                print.memberEndLine();
            }
            Console.ReadKey();
        }

        // 비밀번호 별로 보여주는 기능
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

        // 해당 요소(str)의 INDEX를 찾음
        public int findIndex(string str, int mode)
        {
            for (int i = 0; i < sd.MemberList.Count; i++)
            {
                if (mode == 1 && sd.MemberList[i].MemberID == str)
                    return i;
                else if (mode == 2 && sd.MemberList[i].MemberName == str)
                    return i;
            }
            return -1;
        }

    } // Class - Management
}
