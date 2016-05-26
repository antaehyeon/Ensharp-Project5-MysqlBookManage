using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_BookStore
{
    class BookVO
    {
        private string bookNo;
        private string bookName;
        private string bookAuthor;
        private string bookPrice;
        private string bookQuantity;
        private string bookRentTime;
        private string bookRentID;

        public BookVO() { }
        public BookVO(string bookNo, string bookName, string bookAuthor, string bookPrice, string bookQuantity)
        {
            this.bookNo = bookNo;
            this.bookName = bookName;
            this.bookAuthor = bookAuthor;
            this.bookPrice = bookPrice;
            this.bookQuantity = bookQuantity;
            this.bookRentTime = "";
            this.bookRentID = "";
        }

        public string BookNo
        {
            get { return bookNo; }
            set { bookNo = value; }
        }

        public string BookName
        {
            get { return bookName; }
            set { bookName = value; }
        }

        public string BookAuthor
        {
            get { return bookAuthor; }
            set { bookAuthor = value; }
        }

        public string BookPrice
        {
            get { return bookPrice; }
            set { bookPrice = value; }
        }

        public string BookQuantity
        {
            get { return bookQuantity; }
            set { bookQuantity = value; }
        }

        public string BookRentTime
        {
            get { return bookRentTime; }
            set { bookRentTime = value; }
        }

        public string BookRentID
        {
            get { return bookRentID; }
            set { bookRentID = value; }
        }
    }
}
