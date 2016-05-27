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
        
        private BookVO bookData;

        // MySql
        string strConn = "Server=localhost; Database=bookmanage; Uid=root; Pwd=xogus1696";
        MySqlConnection conn;
        MySqlCommand cmd;

        public SharingData()
        {
            BookData = new BookVO();
            // MySql 연결
            conn = new MySqlConnection(strConn);
        }

        public static SharingData GetInstance()
        {
            if (sdata == null) sdata = new SharingData();
            return sdata;
        }

        internal BookVO BookData
        {
            get { return bookData; }
            set { bookData = value; }
        }

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

        public void insertBookRentNo(string BookNo)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO rent (Fno) VALUES ('" + BookNo + "');";
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void insertLog(string str)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO log (log) VALUES ('" + str + "');";
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public int countRow(string table)
        {
            conn.Open();
            cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM " + table;

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();

            return count;
        }

        // DB에서 존재하는 ID인지 판별
        // 존재 : true 반환
        // 없음 : flase 반환
        public bool selectForExists(string table, string field, string param)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT EXISTS (SELECT * FROM " +table+ " WHERE " +field+ " = '" +param+ "');";
            int result = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            // DB에서 ID의 중복이 존재할 경우 true 리턴
            if (result == 1) { return true; }
            return false;
        }

        public void update(string table, string modifyField, string modifyData, string conditionField, string conditionData )
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE " + table + " SET " + modifyField + " = '" + modifyData + "' WHERE " + conditionField + " = '" + conditionData + "';";
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void delete(string table, string field, string param)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM " +table+ " WHERE "+field+" = '" +param+ "';";
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        // field : 테이블의 찾을 항목
        // param : 찾을 정보
        // needInfo : 필요한 정보
        public string select(string table, string field, string param, string needInfoTable)
        {
            DataSet ds = new DataSet();

            string sql = "SELECT * FROM " +table+ " WHERE " +field+ " = '" +param+ "'";
            MySqlDataAdapter adpt = new MySqlDataAdapter(sql, conn);
            adpt.Fill(ds, table);
            if (ds.Tables.Count > 0)
            {
                foreach(DataRow r in ds.Tables[0].Rows)
                {
                    return param = Convert.ToString(r[needInfoTable]);
                }
            }
            return null;
        }

        // 전체정보 가져오기
        public DataSet selectAll(string table)
        {
            DataSet ds = new DataSet();

            string sql = "SELECT * FROM " + table;
            MySqlDataAdapter adpt = new MySqlDataAdapter(sql, conn);
            adpt.Fill(ds, table);

            return ds;
        }

        // 조건으로 전부 SELECT
        public DataSet selectCondition(string table, string field, string param)
        {
            DataSet ds = new DataSet();

            string sql = "SELECT * FROM " +table+ " WHERE " +field+ " = '" +param+ "'";
            MySqlDataAdapter adpt = new MySqlDataAdapter(sql, conn);
            adpt.Fill(ds, "member");

            return ds;
        }
        

    }
}
