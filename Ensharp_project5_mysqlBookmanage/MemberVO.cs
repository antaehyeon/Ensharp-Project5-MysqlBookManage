using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_BookStore
{
    class MemberVO
    {
        private string memberID;
        private string memberName;
        private string createTime;
        private string pw;
        private string phoneNum;

        public MemberVO() { }
        public MemberVO(string memberID, string memberName, string createTime, string PW, string phoneNum)
        {
            this.memberID = memberID;
            this.memberName = memberName;
            this.createTime = createTime;
            this.pw = PW;
            this.PhoneNum = phoneNum;
        }

        public string MemberID
        {
            get { return memberID; }
            set { memberID = value; }
        }

        public string MemberName
        {
            get { return memberName; }
            set { memberName = value; }
        }

        public string CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        public string PW
        {
            get { return pw; }
            set { pw = value; }
        }

        public string PhoneNum
        {
            get { return phoneNum; }
            set { phoneNum = value; }
        }
    }
}
