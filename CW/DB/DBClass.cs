using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using CW.Data;

namespace CW.DB
{
    class DBClass
    {
        public SqliteConnection conn;
        public SqliteCommand cmd;
        public SqliteDataReader reader;
        public void connect()
        {
            string path = "Data Source=.\\DB\\db1.db";
            conn = new SqliteConnection(path);
            conn.Open();
        }
        public void close()
        {
            if (reader != null)
            {
                if (!reader.IsClosed)
                    reader.Close();

                if (!conn.State.ToString().Equals("Closed"))
                    conn.Close();
            }
        }
        public DataTable getInstitutes()
        {
            DataTable dt = new();
            connect();
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Институты";
            reader = cmd.ExecuteReader();
            dt.Load(reader);
            close();
            return dt;
        }
        public DataTable getGroups(int id)
        {
            DataTable dt = new();
            connect();
            cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT * FROM Группы WHERE Институты_id = {id}";
            reader = cmd.ExecuteReader();
            dt.Load(reader);
            close();
            return dt;
        }
        public DataTable getDisciplines()
        {
            DataTable dt = new();
            connect();
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Дисциплины";
            reader = cmd.ExecuteReader();
            dt.Load(reader);
            close();
            return dt;
        }
        public Sheet getSheet(Sheet sheet)
        {
            connect();
            cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT * FROM Ведомость WHERE Группы_id = {sheet.group.id} " +
                $"AND Дисциплины_id = {sheet.discipline.id} AND Институты_id = {sheet.institute.id}";
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                sheet.id = (int)(long)reader.GetValue(0);
                sheet.type = reader.GetValue(1).ToString();
            }
            close();
            return sheet;
        }
        public DataTable getStudents(Sheet sheet)
        {
            DataTable dt = new();
            connect();
            cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT id, name, surname, thName FROM Студенты WHERE Группа_id = {sheet.group.id}";
            reader = cmd.ExecuteReader();
            dt.Load(reader);
            close();
            return dt;
        }
        public void addSheet(Sheet sheet)
        {
            connect();
            cmd = conn.CreateCommand();
            cmd.CommandText = $"INSERT INTO Ведомость (тип, Группы_id, Дисциплины_id, Институты_id) VALUES ('{sheet.type}', {sheet.group.id}, {sheet.discipline.id}, {sheet.institute.id})";
            cmd.ExecuteNonQuery();
            close();
        }
        public void addMarks(List<string> marks, List<int> students, int id_sheet)
        {
            connect();
            cmd = conn.CreateCommand();
            for(int i = 0; i < marks.Count; i++)
            {
                cmd.CommandText = $"INSERT INTO ОценкиИзВедомостей (Студент_id, Ведомость_id, Результат) VALUES ({students[i]}, {id_sheet}, {marks[i]})";
                cmd.ExecuteNonQuery();
            }
            close();
        }
        public DataTable getFailed(string dis)
        {
            DataTable dt = new();
            connect();
            cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT Дисциплины.name, ОценкиИзВедомостей.Ведомость_id, ОценкиИзВедомостей.Результат FROM Дисциплины LEFT JOIN Ведомость " +
                $"ON Дисциплины.id = Ведомость.Дисциплины_id LEFT JOIN ОценкиИзВедомостей ON Ведомость.id = ОценкиИзВедомостей.Ведомость_id " +
                $"WHERE (ОценкиИзВедомостей.Результат = '2' OR ОценкиИзВедомостей.Результат = '-1') AND Дисциплины.name = '{dis}'";
            reader = cmd.ExecuteReader();
            dt.Load(reader);
            close();
            return dt;
        }
        public DataTable getMarks(string ins, string mark)
        {
            DataTable dt = new();
            connect();
            cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT ОценкиИзВедомостей.Результат FROM ОценкиИзВедомостей LEFT JOIN Ведомость " +
                $"ON ОценкиИзВедомостей.Ведомость_id = Ведомость.id WHERE Ведомость.Институты_id = {int.Parse(ins)} AND ОценкиИзВедомостей.Результат = '{mark}'";
            reader = cmd.ExecuteReader();
            dt.Load(reader);
            close();
            return dt;
        }
    }
}