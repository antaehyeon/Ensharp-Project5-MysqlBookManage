using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

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
        private string createTime;

        DataSet ds;

        public MemberManagement(Run run)
        {
            exception = new Exception();
            print = new Print(run);
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

            //MemberVO data = new MemberVO(ID, name, creatTime, PW, phoneNum);
            sd.memberInfoInsert(ID, name, phoneNum, PW, creatTime);
            print.idRegisterSccessMessage(ID, name, phoneNum, creatTime);

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

        // 회원수정 - 로그인하는 부분
        public void modifyMember()
        {
            string enterPW;
            bool check = false;
            Console.Clear();
            print.modifyMessage();
            ID = Console.ReadLine();
            if (ID == "b") run.startMember();

            ds = sd.selectCondition("member", "ID", ID);

            // 아이디를 데이터베이스에서 긁어오는 부분
            // 아이디를 긁어오면서 패스워드도 같이 저장후 아래서 비교
            for (int i = 0; i < ds.Tables.Count; i++)
            {          
                foreach (DataRow r in ds.Tables[i].Rows)
                {
                    if (Convert.ToString(r["ID"]) == ID)
                    {
                        print.enterPwForModify();
                        PW = Convert.ToString(r["PW"]);
                        check = true;
                        break;
                    }
                }
                if (check) { break; }
            }

            // 아이디를 못찾을경우
            if (!check)
            {
                print.notFindIdMessage();
                modifyMember();
            }

            // 비밀번호 입력부분
            enterPW = showStarPW();
            if (PW == "b") run.startMember();
            if (enterPW == PW)
            {
                run.modifyMenu();
            }
            else
            {
                print.noMatchPW();
                modifyMember();
            }
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
            sd.update("member", "Name", name, "ID", ID);
            print.modifySuccessResult();
            run.modifyMenu();
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
            sd.update("member", "PhoneNumber", phoneNum, "ID", ID);
            print.modifySuccessResult();
            run.modifyMenu();
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
            sd.update("member", "PW", PW, "ID", ID);
            print.modifySuccessResult();
            run.modifyMenu();
        }

        // 회원삭제
        public void deleteMember()
        {
            print.enterIdForDelete();
            ID = Console.ReadLine();
            if (ID == "b") run.startMember();
            bool existCheck = sd.selectForExists("member", "ID", ID);
            if (!existCheck) // MemberList에 ID가 없을경우
            {
                print.notFindIdMessage();
                deleteMember();
            }
            while(true)
            {
                print.enterPwForDelete();
                PW = showStarPW();
                if (PW == "b") run.startMember();

                // select : ID 필드에서 입력된 ID에 해당하는 PW를 찾는다
                if (PW == sd.select("member", "ID", ID, "PW"))
                {
                    sd.delete("member", "ID", ID);
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
            bool exist = sd.selectForExists("member", "ID", ID);
            if (exist)
            {
                // 해당 정보를 SELECT 구문으로 정보를 긁어온다
                name = sd.select("member",  "ID", ID, "Name");
                phoneNum = sd.select("member", "ID", ID, "PhoneNumber");
                createTime = sd.select("member", "ID", ID, "CreateTime");

                // 해당 정보를 넘겨주고 출력
                print.searchIdResult(ID, name, phoneNum, createTime);
                run.searchMenu();
            }
            else
            {
                print.notFindIdMessage();
                searchIdFunction();
            }

        }

        // 회원검색 - 이름
        public void searchNameFunction()
        {
            print.enterNameForSearch();
            name = Console.ReadLine();
            if (name == "b") run.searchMenu();

            print.memberTitle();

            ds = sd.selectCondition("member", "Name", name);
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    ID = ID = Convert.ToString(r["ID"]);
                    name = Convert.ToString(r["Name"]);
                    phoneNum = Convert.ToString(r["PhoneNumber"]);
                    createTime = Convert.ToString(r["CreateTime"]);
                    print.memberResult(ID, name, phoneNum, createTime);
                    print.memberEndLine();
                }
            }
            else
            {
                print.noPrintInfoMessage();
                run.startMember();
            }
            Console.ReadKey();
        }

        // 회원목록출력
        public void printMember()
        {
            ds = sd.selectAll("member");
            print.memberListTitle();
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    ID = Convert.ToString(r["ID"]);
                    name = Convert.ToString(r["Name"]);
                    phoneNum = Convert.ToString(r["PhoneNumber"]);
                    createTime = Convert.ToString(r["CreateTime"]);
                    print.memberResult(ID, name, phoneNum, createTime);
                    print.memberEndLine();
                }
            }
            else
            {
                print.noPrintInfoMessage();
                run.startMember();
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
    } // Class - Management
}
