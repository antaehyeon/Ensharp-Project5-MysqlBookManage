using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensharp_project5_mysqlBookmanage
{
    class BookRentVO
    {
        private int bookNo;
        private string bookRentId;
        private string bookRentTime;

        public BookRentVO() { }
        public BookRentVO(int bookNo, string bookRentId, string bookRentTime)
        {
            this.BookNo = bookNo;
            this.bookRentId = bookRentId;
            this.bookRentTime = bookRentTime;
        }

        public int BookNo
        {
            get { return bookNo; }
            set { bookNo = value; }
        }

        public string BookRentTime
        {
            get { return bookRentTime; }
            set { bookRentTime = value; }
        }

        public string BookRentID
        {
            get { return bookRentId; }
            set { bookRentId = value; }
        }
    }
}
