using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace DapperDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string sql = "Data Source = .;Initial Catalog = Test;User Id = sa;Password = Abc111111;";

            #region insert
            //insert book
            //Models.Book book = new Models.Book
            //{
            //    Name = "",
            //    Author = "",
            //    Press = ""
            //};
            //string query = "insert into Book (Name, Author, Press) values (@Name, @Author, @Press)";
            //using (IDbConnection conn = new SqlConnection(sql))
            //{
            //    int result = conn.Execute(query, book);
            //    Console.WriteLine("result : " + result);
            //}

            //insert bookreview
            //Models.BookReview bookReview = new Models.BookReview
            //{
            //    BookId = 1,
            //    Content = "强烈推荐"
            //};
            ////string query = "insert into Book (Name, Author, Press) values (@Name, @Author, @Press)";
            //string query = "insert into bookreview (bookid, content) values (@bookid, @content)";
            //using (IDbConnection conn = new SqlConnection(sql))
            //{
            //    int result = conn.Execute(query, bookReview);
            //    Console.WriteLine("result : " + result);
            //}
            #endregion

            #region delete
            //string query = "delete from book where id = @id";
            //using (IDbConnection conn = new SqlConnection(sql))
            //{
            //    int result = conn.ExecuteAsync(query, new { id = 3 }).Result;
            //    Console.WriteLine("result : " + result);
            //}
            #endregion

            #region update
            //string query = "update book set author = @author, press = @press where id = @id";
            //Models.Book book = new Models.Book() {
            //    Id = 1,
            //    Author = "黄仁宇",
            //    Press = "三联书店"
            //};
            //using (IDbConnection conn = new SqlConnection(sql))
            //{
            //    int result = conn.ExecuteAsync(query, book).Result;
            //    Console.WriteLine("result : " + result);
            //}
            #endregion

            #region query
            ////无参数查询
            //Console.WriteLine("无参数查询");
            //string query = "select * from book";
            //using(IDbConnection conn = new SqlConnection(sql))
            //{
            //    List<Models.Book> books = conn.Query<Models.Book>(query).ToList();
            //    foreach(Models.Book book in books)
            //    {
            //        Console.WriteLine(book.ToString());
            //    }
            //}
            ////有参数查询
            //Console.WriteLine("有参数查询");
            //query = "select * from book where id = @id";
            //using(IDbConnection conn = new SqlConnection(sql))
            //{
            //    Models.Book book = conn.Query<Models.Book>(query, new { id = 1 }).SingleOrDefault();
            //    Console.WriteLine(book.ToString());
            //}
            #endregion

            #region advanced query
            //Models.Book lookup = null;
            //using (IDbConnection conn = new SqlConnection(sql))
            //{
            //1--N
            //string query = "select * from book b left join bookreview br on br.bookid = b.id where b.id = @id";
            //Query<TFirst, TSecond, TReturn>
            //Models.Book book = conn.Query<Models.Book, Models.BookReview, Models.Book>(query, (bo, bookreview) =>
            //{
            //    if (lookup == null || lookup.Id != bo.Id)
            //        lookup = bo;
            //    if (bookreview != null)
            //        lookup.Reviews.Add(bookreview);
            //    return lookup;
            //}, new { id = 1 }).Distinct().SingleOrDefault();
            //Console.WriteLine("本书信息：");
            //Console.WriteLine(book);
            //Console.WriteLine("本书书评：");
            //foreach (Models.BookReview re in book.Reviews)
            //{
            //    Console.WriteLine(re.ToString());
            //}

            //N--N
            //string query = "select * from book b left join bookreview br on br.bookid = b.id";
            //List<Models.Book> books = conn.Query<Models.Book, Models.BookReview, Models.Book>(query, (bo, bookreview) =>
            //{
            //    if (lookup == null || lookup.Id != bo.Id)
            //        lookup = bo;
            //    if (bookreview != null)
            //        lookup.Reviews.Add(bookreview);
            //    return lookup;
            //}, new { }).Distinct().ToList();
            //Console.WriteLine("本书书评：");
            //foreach (Models.Book bo in books)
            //{
            //    Console.WriteLine(bo.ToString());
            //    foreach (Models.BookReview br in bo.Reviews)
            //    {
            //        Console.WriteLine(br.ToString());
            //    }
            //}

            //1--1
            //Models.BookReview bookReview = new Models.BookReview();
            //string query = "select * from bookreview br left join book b on b.id = br.bookid where br.id = @id";
            //bookReview = conn.Query<Models.BookReview, Models.Book, Models.BookReview>(query, (br, book) =>
            //{
            //    br.AssoicationWithBook = book;
            //    return br;
            //}, new { id = 1 }, splitOn: "Id").SingleOrDefault();
            //Console.WriteLine(bookReview.ToString());
            //Console.WriteLine(bookReview.AssoicationWithBook.ToString());
            //}
            #endregion

            #region transaction
            string query = "insert into Book (Name, Author, Press) values (@Name, @Author, @Press)";
            using (IDbConnection conn = new SqlConnection(sql))
            {
                conn.Open();
                IDbTransaction transaction = conn.BeginTransaction();
                try
                {
                    Models.Book book = new Models.Book
                    {
                        Name = "三国演义",
                        Author = "罗贯中",
                        Press = "不详"
                    };
                    conn.Execute(query, book, transaction);
                    query = "insert into bookreview (bookid, content) values (@bookid, @content)";
                    Models.BookReview bookReview = new Models.BookReview
                    {
                        BookId = 4,
                        Content = "很不错的一本书"
                    };
                    conn.Execute(query, bookReview, transaction);
                    transaction.Commit();
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            #endregion

            Console.Read();
        }
    }
}
