using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;


namespace Project2_BookStore
{
    class SharingData
    {
        private static SharingData sdata;
        private List<MemberVO> memberList;
        private List<BookVO> bookList;

        private int registeredBookQuantity = 0;

        // MySql
        string strConn = "Server=localhost; Database=bookmanage; Uid=root; Pwd=xogus1696";
        MySqlConnection conn;
        MySqlCommand cmd;

        public SharingData()
        {
            MemberList = new List<MemberVO>();
            BookList = new List<BookVO>();
            // MySql 연결
            conn = new MySqlConnection(strConn);
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

        /*
            conn.Open();
            cmd = conn.CreateCommand();

            cmd.ExecuteNonQuery();
            conn.Close();
         */

        //            int result = Convert.ToInt32(cmd.ExecuteScalar());

        // INSERT 멤버 정보
        public void memberInfoInsert(string Id, string Name, string PhoneNumber, string PW, string createTime)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO member(ID, Name, PhoneNumber, PW, CreateTime) VALUES (@ID, @Name, @PhoneNumber, @PW, @CreateTime);";
            cmd.Parameters.Add("@ID", MySqlDbType.VarChar).Value = Id;
            cmd.Parameters.Add("@Name", MySqlDbType.VarChar).Value = Name;
            cmd.Parameters.Add("@PhoneNumber", MySqlDbType.VarChar).Value = PhoneNumber;
            cmd.Parameters.Add("@PW", MySqlDbType.VarChar).Value = PW;
            cmd.Parameters.Add("@CreateTime", MySqlDbType.VarChar).Value = createTime;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        // INSERT 책 정보
        public void bookInfoInsert(string BookNo, string Name, string Author, string Price, string Quantity)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO book(BookNo, Name, Author, Price, Quantity) VALUES (@BookNo, @Name, @Author, @Price, @Quantity);";
            cmd.Parameters.Add("@BookNo", MySqlDbType.VarChar).Value = BookNo;
            cmd.Parameters.Add("@Name", MySqlDbType.VarChar).Value = Name;
            cmd.Parameters.Add("@Author", MySqlDbType.VarChar).Value = Author;
            cmd.Parameters.Add("@Price", MySqlDbType.VarChar).Value = Price;
            cmd.Parameters.Add("@Quantity", MySqlDbType.VarChar).Value = Quantity;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        // DB에서 존재하는 ID인지 판별
        // 존재 : true 반환
        // 없음 : flase 반환
        public bool selectIdForExists(string Id)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT EXISTS (SELECT * FROM member WHERE ID = @ID)";
            cmd.Parameters.Add("@ID", MySqlDbType.VarChar).Value = Id;
            int result = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            // DB에서 ID의 중복이 존재할 경우
            if (result == 1) { return true; }
            return false;
        }

        public void modifyMember(string field, string param, string Id)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE member SET "+field+" = @param WHERE ID = @ID";
            cmd.Parameters.Add("@param", MySqlDbType.VarChar).Value = param;
            cmd.Parameters.Add("@ID", MySqlDbType.VarChar).Value = Id;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void deleteMember(string Id)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM member WHERE ID = @ID";
            cmd.Parameters.Add("@ID", MySqlDbType.VarChar).Value = Id;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void SelectData()
        {
            DataSet ds = new DataSet();

            string sql = "SELECT * FROM member";
            MySqlDataAdapter adpt = new MySqlDataAdapter(sql, conn);
            adpt.Fill(ds, "member");
            if (ds.Tables.Count > 0)
            {
                foreach(DataRow r in ds.Tables[0].Rows)
                {
                    Console.Write(r["ID"]);
                    Console.Write(r["Name"]);
                    Console.Write(r["PhoneNumber"]);
                    Console.Write(r["PW"]);
                    Console.Write(r["CreateTime"]);
                }
            }
        }




    }
}
