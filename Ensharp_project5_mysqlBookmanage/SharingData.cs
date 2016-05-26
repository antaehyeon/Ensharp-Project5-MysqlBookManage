using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_BookStore
{
    class SharingData
    {
        private static SharingData sdata;
        private List<MemberVO> memberList;
        private List<BookVO> bookList;

        private int registeredBookQuantity = 0;

        public SharingData()
        {
            MemberList = new List<MemberVO>();
            BookList = new List<BookVO>();
        }

        public static SharingData GetInstance()
        {
            if (sdata == null) sdata = new SharingData();
            return sdata;
        }

        internal List<MemberVO> MemberList
        {
            get { return memberList; }
            set { memberList = value; }
        }

        internal List<BookVO> BookList
        {
            get { return bookList; }
            set { bookList = value; }
        }

        public int RegisteredBookQuantity
        {
            get { return registeredBookQuantity; }
            set { registeredBookQuantity = value; }
        }
    }
}
