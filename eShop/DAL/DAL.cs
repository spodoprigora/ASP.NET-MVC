 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Common;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using eShop.Models;


namespace eShop.DAL
{
    public static class DAL
    {
        private static ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["DefaultConnection"];

        public static List<CategoryModel> GetCategorys()
        {
            List<CategoryModel> Categorys = new List<CategoryModel>();
            try
            {

                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();
                    // Работа с базой данных

                    SqlCommand command = new SqlCommand("SELECT * FROM Category", connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        CategoryModel obj = new CategoryModel();
                        obj.Id = (int)reader[0];
                        obj.Name = (string)reader[1];
                        Categorys.Add(obj);

                    }
                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
            }

            return Categorys;
        }

        public static List<Product> GetProductByPage(int page, int filtr, string target)
        {
            List<Product> Products = new List<Product>();
            try
            {

                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();
                    // Работа с базой данных
                    SqlCommand command;
                    switch (filtr) // настраиваем фильтр
                    {
                        case 1:
                            if (target == "up")
                                command = new SqlCommand("SELECT * FROM Product WHERE active='True' ORDER BY price DESC OFFSET(4*@Page) ROWS FETCH NEXT 4 ROWS ONLY", connection);
                            else
                                command = new SqlCommand("SELECT * FROM Product WHERE active='True' ORDER BY price OFFSET(4*@Page) ROWS FETCH NEXT 4 ROWS ONLY", connection);
                            break;
                        case 2:
                            if (target == "up")
                                command = new SqlCommand("SELECT p.id, p.idСategory,p.name, p.price, p.description, p.image FROM Product AS p, ProductCharacteristics as pCh WHERE active='True' AND p.id = pCh.idProduct AND pCh.idCharacteristic = 2 ORDER BY pCh.decimalValue DESC OFFSET (4*@Page) ROWS FETCH NEXT 4 ROWS ONLY", connection);
                            else
                                command = new SqlCommand("SELECT p.id, p.idСategory,p.name, p.price, p.description, p.image FROM Product AS p, ProductCharacteristics as pCh WHERE active='True' AND p.id = pCh.idProduct AND pCh.idCharacteristic = 2 ORDER BY pCh.decimalValue OFFSET (4*@Page) ROWS FETCH NEXT 4 ROWS ONLY", connection);
                            break;
                        case 3:
                            if (target == "up")
                                command = new SqlCommand("SELECT p.id, p.idСategory,p.name, p.price, p.description, p.image FROM Product AS p, ProductCharacteristics as pCh WHERE active='True' AND p.id = pCh.idProduct AND pCh.idCharacteristic = 1 ORDER BY pCh.decimalValue DESC OFFSET (4*@Page) ROWS FETCH NEXT 4 ROWS ONLY", connection);
                            else
                                command = new SqlCommand("SELECT p.id, p.idСategory,p.name, p.price, p.description, p.image FROM Product AS p, ProductCharacteristics as pCh WHERE active='True' AND p.id = pCh.idProduct AND pCh.idCharacteristic = 1 ORDER BY pCh.decimalValue OFFSET (4*@Page) ROWS FETCH NEXT 4 ROWS ONLY", connection);
                            break;
                        case 4:
                            if (target == "up")
                                command = new SqlCommand("SELECT p.id, p.idСategory,p.name, p.price, p.description, p.image FROM Product AS p, ProductCharacteristics as pCh WHERE active='True' AND p.id = pCh.idProduct AND pCh.idCharacteristic = 3 ORDER BY pCh.decimalValue DESC OFFSET (4*@Page) ROWS FETCH NEXT 4 ROWS ONLY", connection);
                            else
                                command = new SqlCommand("SELECT p.id, p.idСategory,p.name, p.price, p.description, p.image FROM Product AS p, ProductCharacteristics as pCh WHERE active='True' AND p.id = pCh.idProduct AND pCh.idCharacteristic = 3 ORDER BY pCh.decimalValue OFFSET (4*@Page) ROWS FETCH NEXT 4 ROWS ONLY", connection);
                            break;
                        default:
                            command = new SqlCommand("SELECT * FROM Product WHERE active='True' ORDER BY id OFFSET(4*@Page) ROWS FETCH NEXT 4 ROWS ONLY", connection);
                            break;
                    }

                    //SqlCommand command = new SqlCommand("SELECT TOP(4) * FROM Product WHERE Product.id NOT IN (SELECT TOP(4*@Page) Product.id FROM Product )",  connection);


                    SqlParameter Page = new SqlParameter("Page", SqlDbType.Int);
                    Page.Value = page;
                    command.Parameters.Add(Page);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Product obj = new Product();
                        obj.Id = (int)reader[0];
                        obj.IdCategory = (int)reader[1];
                        obj.Name = (string)reader[2];
                        obj.Price = (decimal)reader[3];
                        obj.Description = (string)reader[4];
                        obj.Image = (string)reader[5];
                        Products.Add(obj);

                    }
                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
            }

            return Products;

        }
 
        public static List<Product> GetProductByCategoryIdPage(int category, int page, int filtr, string target)
        {
            List<Product> Products = new List<Product>();
            try
            {

                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();
                    // Работа с базой данных
                    SqlCommand command;
                    switch (filtr)
                    {
                        case 1:
                            if (target == "up")
                                command = new SqlCommand("SELECT * FROM Product WHERE active='True' AND Product.idСategory = @CategoryID  ORDER BY price DESC OFFSET(4*@Page) ROWS FETCH NEXT 4 ROWS ONLY", connection);
                            else
                                command = new SqlCommand("SELECT * FROM Product WHERE active='True' AND Product.idСategory = @CategoryID ORDER BY price OFFSET(4*@Page) ROWS FETCH NEXT 4 ROWS ONLY", connection);
                            break;
                        case 2:
                            if (target == "up")
                                command = new SqlCommand("SELECT p.id, p.idСategory,p.name, p.price, p.description, p.image FROM Product AS p, ProductCharacteristics as pCh WHERE active='True' AND p.idСategory = @CategoryID AND p.id = pCh.idProduct AND pCh.idCharacteristic = 2 ORDER BY pCh.decimalValue DESC OFFSET(4*@Page) ROWS FETCH NEXT 4 ROWS ONLY", connection);
                            else
                                command = new SqlCommand("SELECT p.id, p.idСategory,p.name, p.price, p.description, p.image FROM Product AS p, ProductCharacteristics as pCh WHERE active='True' AND p.idСategory = @CategoryID AND p.id = pCh.idProduct AND pCh.idCharacteristic = 2 ORDER BY pCh.decimalValue OFFSET(4*@Page) ROWS FETCH NEXT 4 ROWS ONLY", connection);
                            break;
                        case 3:
                            if (target == "up")
                                command = new SqlCommand("SELECT p.id, p.idСategory,p.name, p.price, p.description, p.image FROM Product AS p, ProductCharacteristics as pCh WHERE active='True' AND p.idСategory = @CategoryID AND p.id = pCh.idProduct AND pCh.idCharacteristic = 1 ORDER BY pCh.decimalValue DESC OFFSET(4*@Page) ROWS FETCH NEXT 4 ROWS ONLY", connection);
                            else
                                command = new SqlCommand("SELECT p.id, p.idСategory,p.name, p.price, p.description, p.image FROM Product AS p, ProductCharacteristics as pCh WHERE active='True' AND p.idСategory = @CategoryID AND p.id = pCh.idProduct AND pCh.idCharacteristic = 1 ORDER BY pCh.decimalValue OFFSET(4*@Page) ROWS FETCH NEXT 4 ROWS ONLY", connection);
                            break;
                        case 4:
                            if (target == "up")
                                command = new SqlCommand("SELECT p.id, p.idСategory,p.name, p.price, p.description, p.image FROM Product AS p, ProductCharacteristics as pCh WHERE active='True' AND p.idСategory = @CategoryID AND p.id = pCh.idProduct AND pCh.idCharacteristic = 3 ORDER BY DESC pCh.decimalValue OFFSET(4*@Page) ROWS FETCH NEXT 4 ROWS ONLY", connection);
                            else
                                command = new SqlCommand("SELECT p.id, p.idСategory,p.name, p.price, p.description, p.image FROM Product AS p, ProductCharacteristics as pCh WHERE active='True' AND p.idСategory = @CategoryID AND p.id = pCh.idProduct AND pCh.idCharacteristic = 3 ORDER BY pCh.decimalValue OFFSET(4*@Page) ROWS FETCH NEXT 4 ROWS ONLY", connection);
                            break;
                        default:
                            command = new SqlCommand("SELECT * FROM Product WHERE active='True' AND Product.idСategory = @CategoryID ORDER BY id OFFSET(4*@Page) ROWS FETCH NEXT 4 ROWS ONLY", connection);
                            break;

                    }

                    // SqlCommand command = new SqlCommand("SELECT TOP(4) * FROM Product WHERE Product.idСategory = @CategoryID AND Product.id NOT IN (SELECT TOP(4*@Page) Product.id FROM Product WHERE Product.idСategory = @CategoryID )", connection);
                    // SqlCommand command = new SqlCommand("SELECT * FROM Product WHERE Product.idСategory = @CategoryID ORDER BY id OFFSET(4*@Page) ROWS FETCH NEXT 4 ROWS ONLY", connection);

                    SqlParameter Page = new SqlParameter("Page", SqlDbType.Int);
                    Page.Value = page;
                    command.Parameters.Add(Page);
                    SqlParameter CategoryID = new SqlParameter("categoryID", SqlDbType.Int);
                    CategoryID.Value = category;
                    command.Parameters.Add(CategoryID);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Product obj = new Product();
                        obj.Id = (int)reader[0];
                        obj.IdCategory = (int)reader[1];
                        obj.Name = (string)reader[2];
                        obj.Price = (decimal)reader[3];
                        obj.Description = (string)reader[4];
                        obj.Image = (string)reader[5];
                        Products.Add(obj);

                    }
                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
            }

            return Products;

        }

        public static Product GetProductById(int ID)
        {
            Product obj = new Product();
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();
                    // Работа с базой данных
                    SqlCommand command = new SqlCommand("SELECT * FROM Product WHERE Product.id = @ProductID", connection);
                    SqlParameter ProductID = new SqlParameter("productID", SqlDbType.Int);
                    ProductID.Value = ID;
                    command.Parameters.Add(ProductID);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        obj.Id = (int)reader[0];
                        obj.IdCategory = (int)reader[1];
                        obj.Name = (string)reader[2];
                        obj.Price = (decimal)reader[3];
                        obj.Description = (string)reader[4];
                        obj.Image = (string)reader[5];
                    }

                    reader.Close();
                    //        obj = (Product)command.ExecuteScalar();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return obj;
        }

        public static CategoryModel GetCategory(int IdCategory)
        {

            CategoryModel Category = new CategoryModel();
            try
            {

                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();
                    // Работа с базой данных


                    SqlCommand command = new SqlCommand("SELECT * FROM Category WHERE Category.id = @IdCategory", connection);
                    SqlParameter CategoryId = new SqlParameter("IdCategory", SqlDbType.Int);
                    CategoryId.Value = IdCategory;
                    command.Parameters.Add(CategoryId);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Category.Id = (int)reader[0];
                        Category.Name = (string)reader[1];
                    }
                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
            }

            return Category;


        }

        public static IEnumerable<KeyValuePair<eShop.Models.Product, int>> GetBasketProductFromSesion(Dictionary<int, int> basket)
        {
            var prod = from item in basket
                       select new KeyValuePair<Product, int>
                         (
                            GetProductById(item.Key),
                            item.Value
                        );
            return prod;
        }

        public static decimal CalcSumma(IEnumerable<KeyValuePair<Product, int>> prod)
        {
            decimal summa = 0;
            foreach (var Product in prod)
                summa += Product.Key.Price * Product.Value;
            return summa;
        }

        public static void InsertToBasket(Basket item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();
                    // Работа с базой данных
                    SqlCommand command = new SqlCommand("INSERT INTO BASKET (idProduct, count, orderNumber) VALUES (@IdProduct, @Count, @OrderNumber)", connection);


                    SqlParameter IdProduct = new SqlParameter("IdProduct", SqlDbType.Int);
                    IdProduct.Value = item.IdProduct;
                    command.Parameters.Add(IdProduct);
                    SqlParameter Count = new SqlParameter("Count", SqlDbType.Int);
                    Count.Value = item.Count; ;
                    command.Parameters.Add(Count);
                    SqlParameter OrderNumber = new SqlParameter("OrderNumber", SqlDbType.Int);
                    OrderNumber.Value = item.OrderNumber;
                    command.Parameters.Add(OrderNumber);

                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
            }

        }

        public static int createOrder(string idUser, Dictionary<int, int> basket)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection())
                {

                    int id;

                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();
                    //string Orderdate = (string)DateTime.Now.ToShortDateString();
                    string Orderdate = DateTime.Now.ToString("yyyy.MM.dd");

                    //string[] datmas = Orderdate.Split('.');
                    //Orderdate = datmas[2] + "." + datmas[1] + "." + datmas[0];
                   

                    IEnumerable<KeyValuePair<Product, int>> prod = DAL.GetBasketProductFromSesion(basket);
                    decimal summa = DAL.CalcSumma(prod);
                    // Работа с базой данных
                    SqlCommand command = new SqlCommand("INSERT INTO OrdersList (idUser, OrderDate, idStatus, ChangStatusDate, Price) VALUES (@IdUser, @OrderDate, @idStatus, @OrderDate, @Price)", connection);

                    SqlParameter IdUser = new SqlParameter("IdUser", SqlDbType.NVarChar);
                    IdUser.Value = idUser;
                    command.Parameters.Add(IdUser);


                    SqlParameter idStatus = new SqlParameter("idStatus", SqlDbType.Int);
                    idStatus.Value = 1;
                    command.Parameters.Add(idStatus);

                    SqlParameter OrderDate = new SqlParameter("OrderDate", SqlDbType.NVarChar);
                    OrderDate.Value = Orderdate;
                    command.Parameters.Add(OrderDate);

                    SqlParameter Price = new SqlParameter("Price", SqlDbType.Money);
                    Price.Value = summa;
                    command.Parameters.Add(Price);

                    command.ExecuteNonQuery();



                    SqlCommand command1 = new SqlCommand("SELECT id FROM OrdersList WHERE idUser = @IdUser1 ORDER BY id DESC ", connection);
                    SqlParameter IdUser1 = new SqlParameter("IdUser1", SqlDbType.NVarChar);
                    IdUser1.Value = idUser;
                    command1.Parameters.Add(IdUser1);

                    id = (int)command1.ExecuteScalar();
                    connection.Close();
                    return id;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public static double PageCount(object idCategory)
        {


            int pageSize = 4;
            int count = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();
                    // Работа с базой данных
                    if (idCategory != null)
                    {
                        SqlCommand command = new SqlCommand("SELECT count(*) FROM Product WHERE active='True' AND Product.idСategory = @CategoryID ", connection);
                        SqlParameter CategoryID = new SqlParameter("categoryID", SqlDbType.Int);
                        CategoryID.Value = idCategory;
                        command.Parameters.Add(CategoryID);
                        count = (int)command.ExecuteScalar();
                        connection.Close();

                    }
                    else
                    {
                        SqlCommand command = new SqlCommand("SELECT count(*) FROM Product WHERE active='True'", connection);
                        count = (int)command.ExecuteScalar();
                        connection.Close();
                    }

                }
            }
            catch (Exception ex)
            {
            }
            double val = (float)count / (float)pageSize;
            return Math.Ceiling(val);
        }

        public static List<Characteristicscs> GetCharacteristicByProductId(int id)
        {
            List<Characteristicscs> Characteristicsc = new List<Characteristicscs>();
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();
                    // Работа с базой данных


                    SqlCommand command = new SqlCommand("SELECT Characteristics.name, ProductCharacteristics.stringValue FROM Characteristics, ProductCharacteristics WHERE ProductCharacteristics.idProduct = @ProductId AND ProductCharacteristics.idCharacteristic = Characteristics.id", connection);
                    SqlParameter ProductId = new SqlParameter("ProductId", SqlDbType.Int);
                    ProductId.Value = id;
                    command.Parameters.Add(ProductId);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Characteristicscs obj = new Characteristicscs();
                        obj.Name = (string)reader[0];
                        obj.Value = (string)reader[1];
                        Characteristicsc.Add(obj);
                    }
                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return Characteristicsc;
        }

        public static List<getOrderList> getOrder(int opage, int status = 0, string startDate =null, string endDate = null)
        {
           
            List<getOrderList> Orders = new List<getOrderList>();
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();

                    // Работа с базой данных
                    SqlCommand command = null;
                    if (status != 0 && (startDate == null && endDate == null))
                    {
                        command = new SqlCommand("SELECT o.id, a.Name, a.PhoneNumber, o.OrderDate, s.StatusName, o.ChangStatusDate, o.Price, o.comments, a.Id FROM OrdersList as o, AspNetUsers as a, Status as s WHERE o.idStatus = s.id AND o.idUser = a.Id AND o.idStatus = @Status ORDER BY o.id DESC OFFSET (10*@Opage) ROWS FETCH NEXT 10 ROWS ONLY", connection);

                        SqlParameter Status = new SqlParameter("Status", SqlDbType.Int);
                        Status.Value = status;
                        command.Parameters.Add(Status);
                    }
                    else if (status == 0 && startDate != null && endDate != null)
                    {
                        string[] start = startDate.Split('.');
                        startDate = start[2] + "." + start[1] + "." + start[0];
                        string[] end = endDate.Split('.');
                        endDate = end[2] + "." + end[1] + "." + end[0];

                        command = new SqlCommand("SELECT o.id, a.Name, a.PhoneNumber, o.OrderDate, s.StatusName, o.ChangStatusDate, o.Price, o.comments, a.Id From OrdersList as o, AspNetUsers as a, Status as s Where o.idStatus = s.id AND o.idUser = a.Id AND o.OrderDate BETWEEN @StartDate AND @EndDate  Order by o.id DESC OFFSET (10*@Opage) ROWS FETCH NEXT 10 ROWS ONLY", connection);

                        SqlParameter StartDate = new SqlParameter("StartDate", SqlDbType.NVarChar);
                        StartDate.Value = startDate;
                        command.Parameters.Add(StartDate);

                        SqlParameter EndDate = new SqlParameter("EndDate", SqlDbType.NVarChar);
                        EndDate.Value = endDate;
                        command.Parameters.Add(EndDate);

                    } else if (status != 0 && startDate != null && endDate != null)
                    {
                        string[] start = startDate.Split('.');
                        startDate = start[2] + "." + start[1] + "." + start[0];
                        string[] end = endDate.Split('.');
                        endDate = end[2] + "." + end[1] + "." + end[0];

                        command = new SqlCommand("SELECT o.id, a.Name, a.PhoneNumber, o.OrderDate, s.StatusName, o.ChangStatusDate, o.Price, o.comments, a.Id From OrdersList as o, AspNetUsers as a, Status as s Where o.idStatus = s.id AND o.idUser = a.Id AND o.idStatus = @Status AND o.OrderDate BETWEEN @StartDate AND @EndDate  Order by o.id DESC OFFSET (10*@Opage) ROWS FETCH NEXT 10 ROWS ONLY", connection);

                        SqlParameter StartDate = new SqlParameter("StartDate", SqlDbType.NVarChar);
                        StartDate.Value = startDate;
                        command.Parameters.Add(StartDate);

                        SqlParameter EndDate = new SqlParameter("EndDate", SqlDbType.NVarChar);
                        EndDate.Value = endDate;
                        command.Parameters.Add(EndDate);

                        SqlParameter Status = new SqlParameter("Status", SqlDbType.Int);
                        Status.Value = status;
                        command.Parameters.Add(Status);
                    }
                    else
                    {
                        command = new SqlCommand("SELECT o.id, a.Name, a.PhoneNumber, o.OrderDate, s.StatusName, o.ChangStatusDate, o.Price, o.comments, a.Id From OrdersList as o, AspNetUsers as a, Status as s Where o.idStatus = s.id AND o.idUser = a.Id Order by o.id DESC OFFSET (10*@Opage) ROWS FETCH NEXT 10 ROWS ONLY", connection);

                    }
                    
                    
                    SqlParameter Opage = new SqlParameter("Opage", SqlDbType.Int);
                    Opage.Value = opage;
                    command.Parameters.Add(Opage);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        getOrderList obj = new getOrderList();
                        obj.Id = (int)reader[0];
                        obj.Name = (string)reader[1];
                        obj.Tell = (string)reader[2];
                        
                        string dat = (string)reader[3];
                        string[] datmas = dat.Split('.');
                        string reversDat = datmas[2] + "." + datmas[1] + "." + datmas[0];

                        obj.Orderdate = reversDat;
                        obj.Status = (string)reader[4];
                        
                        dat = (string)reader[5];
                        datmas = dat.Split('.');
                        reversDat = datmas[2] + "." + datmas[1] + "." + datmas[0];
                        obj.ChangeStatusDate = reversDat;

                        obj.Price = (decimal)reader[6];

                        if (!reader.IsDBNull(7)){
                             obj.comments = (string)reader[7];
                        }
                        obj.UserId = (string)reader[8];
                        Orders.Add(obj);

                    }
                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return Orders;
        }

        public static List<Description> getDescription(int id)
        {
            List<Description> Descriptions = new List<Description>();
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();
                    // Работа с базой данных
                    SqlCommand command = new SqlCommand("SELECT Product.image, Product.name, Product.price, Basket.count FROM Basket, Product WHERE Basket.orderNumber =@Id AND Product.id = Basket.idProduct;", connection);
                    SqlParameter Id = new SqlParameter("Id", SqlDbType.Int);
                    Id.Value = id;
                    command.Parameters.Add(Id);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Description obj = new Description();
                        obj.image = (string)reader[0];
                        obj.name = (string)reader[1];
                        obj.price = (decimal)reader[2];
                        obj.count = (int)reader[3];

                        Descriptions.Add(obj);

                    }
                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return Descriptions;


        }

        public static List<string> getStatusList()
        {
            List<string> status = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();
                    // Работа с базой данных
                    SqlCommand command = new SqlCommand("SELECT Status.StatusName FROM Status", connection);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string str = "";
                        str = (string)reader[0];
                        status.Add(str);
                    }
                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return status;
        }

        public static void changeStatus(int id, int status)
        {



            string now = DateTime.Now.ToString("yyy.MM.dd");
            //DateTime now = DateTime.Now.Date;

            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();
                    // Работа с базой данных
                    SqlCommand command = new SqlCommand("UPDATE OrdersList SET idStatus = @Status, ChangStatusDate = @changStatusDate WHERE id=@Id", connection);

                    SqlParameter changStatusDate = new SqlParameter("changStatusDate", SqlDbType.VarChar);
                    changStatusDate.Value = now;
                    command.Parameters.Add(changStatusDate);

                    SqlParameter Status = new SqlParameter("Status", SqlDbType.Int);
                    Status.Value = status;
                    command.Parameters.Add(Status);
                    SqlParameter Id = new SqlParameter("Id", SqlDbType.Int);
                    Id.Value = id; ;
                    command.Parameters.Add(Id);


                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
            }

        }

        public static void changeComent(int id, string coment)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();
                    // Работа с базой данных
                    SqlCommand command = new SqlCommand("UPDATE OrdersList SET comments = @Comment WHERE id=@Id", connection);

                    SqlParameter Comment = new SqlParameter("Comment", SqlDbType.NVarChar);
                    Comment.Value = coment;
                    command.Parameters.Add(Comment);
                   
                    SqlParameter Id = new SqlParameter("Id", SqlDbType.Int);
                    Id.Value = id; ;
                    command.Parameters.Add(Id);


                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
            }

        }

        public static void addCategory(string cat)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();
                    // Работа с базой данных
                    SqlCommand command = new SqlCommand("INSERT INTO Category (name) VALUES (@Name)", connection);

                    SqlParameter Name = new SqlParameter("Name", SqlDbType.NVarChar);
                    Name.Value = cat;
                    command.Parameters.Add(Name);


                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static void delCategory(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();
                    // Работа с базой данных
                    SqlCommand command = new SqlCommand("DELETE FROM Category WHERE id= @Id", connection);

                    SqlParameter Id = new SqlParameter("Id", SqlDbType.Int);
                    Id.Value = id;
                    command.Parameters.Add(Id);


                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void editCategory(int id, string name)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();
                    // Работа с базой данных
                    SqlCommand command = new SqlCommand("UPDATE Category SET name =@Name WHERE id= @Id", connection);

                    SqlParameter Name = new SqlParameter("Name", SqlDbType.NVarChar);
                    Name.Value = name;
                    command.Parameters.Add(Name);

                    SqlParameter Id = new SqlParameter("Id", SqlDbType.Int);
                    Id.Value = id;
                    command.Parameters.Add(Id);


                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static List<editProduct> getEditProduct(int page, int categoryFiltr =0, string activeFiltr = null)
        {
            List<editProduct> editProductList = new List<editProduct>();
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();
                    SqlCommand command = null;
                    // Работа с базой данных
                    if(categoryFiltr!=0 && activeFiltr==null){
                         command = new SqlCommand("SELECT p.id, p.idСategory, c.name, p.image, p.name, p.description, p.price, p.active FROM Product as p, Category as c WHERE p.idСategory =c.id AND p.idСategory = @CategoryFiltr ORDER BY p.id OFFSET (6*@Page) ROWS FETCH NEXT 6 ROWS ONLY", connection);
                         
                         SqlParameter CategoryFiltr = new SqlParameter("CategoryFiltr", SqlDbType.Int);
                         CategoryFiltr.Value = categoryFiltr;
                         command.Parameters.Add(CategoryFiltr);

                         SqlParameter Page = new SqlParameter("Page", SqlDbType.Int);
                         Page.Value = page;
                         command.Parameters.Add(Page);

                    }else if(categoryFiltr==0 && activeFiltr != null){
                        command = new SqlCommand("SELECT p.id, p.idСategory, c.name, p.image, p.name, p.description, p.price, p.active FROM Product as p, Category as c WHERE p.idСategory =c.id  AND p.active=@ActiveFiltr ORDER BY p.id OFFSET (6*@Page) ROWS FETCH NEXT 6 ROWS ONLY", connection);

                        int activeFiltrInt = (activeFiltr == "true") ? 1 : 0;
                        SqlParameter ActiveFiltr = new SqlParameter("ActiveFiltr", SqlDbType.Int);
                        ActiveFiltr.Value = activeFiltrInt;
                        command.Parameters.Add(ActiveFiltr);

                        SqlParameter Page = new SqlParameter("Page", SqlDbType.Int);
                        Page.Value = page;
                        command.Parameters.Add(Page);
                    }
                    else if (categoryFiltr != 0 && activeFiltr != null)
                    {
                        command = new SqlCommand("SELECT p.id, p.idСategory, c.name, p.image, p.name, p.description, p.price, p.active FROM Product as p, Category as c WHERE p.idСategory =c.id AND p.idСategory = @CategoryFiltr AND p.active=@ActiveFiltr ORDER BY p.id OFFSET (6*@Page) ROWS FETCH NEXT 6 ROWS ONLY", connection);
                        int activeFiltrInt = (activeFiltr == "true") ? 1 : 0;
                        SqlParameter ActiveFiltr = new SqlParameter("ActiveFiltr", SqlDbType.Int);
                        ActiveFiltr.Value = activeFiltrInt;
                        command.Parameters.Add(ActiveFiltr);

                        SqlParameter CategoryFiltr = new SqlParameter("CategoryFiltr", SqlDbType.Int);
                        CategoryFiltr.Value = categoryFiltr;
                        command.Parameters.Add(CategoryFiltr);

                        SqlParameter Page = new SqlParameter("Page", SqlDbType.Int);
                        Page.Value = page;
                        command.Parameters.Add(Page);

                    }else{
                         command = new SqlCommand("SELECT p.id, p.idСategory, c.name, p.image, p.name, p.description, p.price, p.active FROM Product as p, Category as c WHERE p.idСategory =c.id ORDER BY p.id OFFSET (6*@Page) ROWS FETCH NEXT 6 ROWS ONLY", connection);
                         SqlParameter Page = new SqlParameter("Page", SqlDbType.Int);
                         Page.Value = page;
                         command.Parameters.Add(Page);
                    }
                   


                    
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        editProduct obj = new editProduct();
                        obj.Id = (int)reader[0];
                        obj.IdCategory = (int)reader[1];
                        obj.NameCategory = (string)reader[2];
                        obj.Image = (string)reader[3];
                        obj.Name = (string)reader[4];
                        obj.Description = (string)reader[5];
                        obj.Price = (decimal)reader[6];
                        obj.active = (bool)reader[7];

                        editProductList.Add(obj);
                    }
                    reader.Close();
                     connection.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return editProductList;
        }

        public static void delProduct(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();
                    // Работа с базой данных
                    SqlCommand command = new SqlCommand("DELETE FROM ProductCharacteristics WHERE idProduct= @Id", connection);
                    SqlParameter Id = new SqlParameter("Id", SqlDbType.Int);
                    Id.Value = id;
                    command.Parameters.Add(Id);
                    command.ExecuteNonQuery();

                    SqlCommand command1 = new SqlCommand("DELETE FROM Product WHERE id= @Id", connection);

                    SqlParameter Id1 = new SqlParameter("Id", SqlDbType.Int);
                    Id1.Value = id;
                    command1.Parameters.Add(Id1);


                    command1.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            { 

            }

        }

        public static void editProduct(int id, int catId, string name, string description, decimal price, string img, bool active)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();
                    // Работа с базой данных

                    SqlCommand command = new SqlCommand("UPDATE Product SET idСategory =@CatId, name = @Name, price = @Price, description = @Description, image = @Img, active = @Active WHERE id= @Id", connection);

                    SqlParameter CatId = new SqlParameter("CatId", SqlDbType.Int);
                    CatId.Value = catId;
                    command.Parameters.Add(CatId);

                    SqlParameter Name = new SqlParameter("Name", SqlDbType.NVarChar);
                    Name.Value = name;
                    command.Parameters.Add(Name);

                    SqlParameter Price = new SqlParameter("Price", SqlDbType.Decimal);
                    Price.Value = price;
                    command.Parameters.Add(Price);

                    SqlParameter Description = new SqlParameter("Description", SqlDbType.NVarChar);
                    Description.Value = description;
                    command.Parameters.Add(Description);

                    SqlParameter Img = new SqlParameter("Img", SqlDbType.NVarChar);
                    Img.Value = img;
                    command.Parameters.Add(Img);

                    SqlParameter Active = new SqlParameter("Active", SqlDbType.Bit);
                    Active.Value = active;
                    command.Parameters.Add(Active);

                    SqlParameter Id = new SqlParameter("Id", SqlDbType.Int);
                    Id.Value = id;
                    command.Parameters.Add(Id);


                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static List<Characteristicscs> getCharacteristics(int charact)
        {
            List<Characteristicscs> Characteristics = new List<Characteristicscs>();
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();
                    // Работа с базой данных
                    SqlCommand command = new SqlCommand("SELECT pc.id, c.name, pc.stringValue, pc.decimalValue FROM ProductCharacteristics as pc, Characteristics as c WHERE pc.idCharacteristic =c.id AND pc.idProduct = @Id", connection);

                    SqlParameter Id = new SqlParameter("Id", SqlDbType.Int);
                    Id.Value = charact;
                    command.Parameters.Add(Id);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Characteristicscs obj = new Characteristicscs();
                        obj.id = (int)reader[0];
                        obj.Name = (string)reader[1];
                        obj.Value = (string)reader[2];
                        if (reader[3] != DBNull.Value)
                            obj.IntValue = (decimal)reader[3];
                        Characteristics.Add(obj);
                    }
                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return Characteristics;
        }

        public static void addProduct(int catId, string name, decimal price, string description, string img, bool active)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();
                    // Работа с базой данных
                    SqlCommand command = new SqlCommand("INSERT INTO Product (idСategory, name, price, description, image, active) VALUES (@IdCat, @Name, @Price, @Description, @Img, @Active)", connection);

                    SqlParameter IdCat = new SqlParameter("IdCat", SqlDbType.Int);
                    IdCat.Value = catId;
                    command.Parameters.Add(IdCat);

                    SqlParameter Name = new SqlParameter("Name", SqlDbType.NVarChar);
                    Name.Value = name;
                    command.Parameters.Add(Name);

                    SqlParameter Price = new SqlParameter("Price", SqlDbType.Decimal);
                    Price.Value = price;
                    command.Parameters.Add(Price);

                    SqlParameter Description = new SqlParameter("Description", SqlDbType.NVarChar);
                    Description.Value = description;
                    command.Parameters.Add(Description);

                    SqlParameter Img = new SqlParameter("Img", SqlDbType.NVarChar);
                    Img.Value = img;
                    command.Parameters.Add(Img);

                    SqlParameter Active = new SqlParameter("Active", SqlDbType.Bit);
                    Active.Value = active;
                    command.Parameters.Add(Active);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
            }

        }

        public static void editCharacteristics(int id1, int id2, int id3, string strVal1, string strVal2, string strVal3, decimal intVal1, decimal intVal2, decimal intVal3)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();
                    // Работа с базой данных

                    SqlCommand command = new SqlCommand("UPDATE ProductCharacteristics SET stringValue =@StringValue, decimalValue = @DecimalValue WHERE id= @Id", connection);

                    SqlParameter StringValue = new SqlParameter("StringValue", SqlDbType.NVarChar);
                    StringValue.Value = strVal1;
                    command.Parameters.Add(StringValue);

                    SqlParameter DecimalValue = new SqlParameter("DecimalValue", SqlDbType.Decimal);
                    DecimalValue.Value = intVal1;
                    command.Parameters.Add(DecimalValue);

                    SqlParameter Id = new SqlParameter("Id", SqlDbType.Decimal);
                    Id.Value = id1;
                    command.Parameters.Add(Id);
                    command.ExecuteNonQuery();

                    SqlCommand command1 = new SqlCommand("UPDATE ProductCharacteristics SET stringValue =@StringValue, decimalValue = @DecimalValue WHERE id= @Id", connection);

                    SqlParameter StringValue1 = new SqlParameter("StringValue", SqlDbType.NVarChar);
                    StringValue1.Value = strVal2;
                    command1.Parameters.Add(StringValue1);
                    SqlParameter DecimalValue1 = new SqlParameter("DecimalValue", SqlDbType.Decimal);
                    DecimalValue1.Value = intVal2;
                    command1.Parameters.Add(DecimalValue1);
                    SqlParameter Id1 = new SqlParameter("Id", SqlDbType.Decimal);
                    Id1.Value = id2;
                    command1.Parameters.Add(Id1);
                    command1.ExecuteNonQuery();

                    SqlCommand command2 = new SqlCommand("UPDATE ProductCharacteristics SET stringValue =@StringValue, decimalValue = @DecimalValue WHERE id= @Id", connection);

                    SqlParameter StringValue2 = new SqlParameter("StringValue", SqlDbType.NVarChar);
                    StringValue2.Value = strVal3;
                    command2.Parameters.Add(StringValue2);
                    SqlParameter DecimalValue2 = new SqlParameter("DecimalValue", SqlDbType.Decimal);
                    DecimalValue2.Value = intVal3;
                    command2.Parameters.Add(DecimalValue2);
                    SqlParameter Id2 = new SqlParameter("Id", SqlDbType.Decimal);
                    Id2.Value = id3;
                    command2.Parameters.Add(Id2);

                    command2.ExecuteNonQuery();




                    connection.Close();
                }

            }
            catch (Exception ex)
            {
            }

        }

        public static void addCharacteristics(int id, string strVal1 =null, string strVal2=null, string strVal3=null, decimal? intVal1=null, decimal? intVal2=null, decimal? intVal3=null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();
                    // Работа с базой данных

                    SqlCommand command = new SqlCommand("INSERT INTO ProductCharacteristics (idProduct, idCharacteristic, stringValue, decimalValue) VALUES(@IdProduct, 1, @StringValue, @DecimalValue)", connection);

                    SqlParameter StringValue = new SqlParameter("StringValue", SqlDbType.NVarChar);
                    StringValue.IsNullable = true;
                    StringValue.Value = strVal1;
                    command.Parameters.Add(StringValue);

                    SqlParameter DecimalValue = new SqlParameter("DecimalValue", SqlDbType.Decimal);
                    DecimalValue.IsNullable = true;
                    if(intVal1==null)
                      DecimalValue.Value = DBNull.Value;
                    else
                        DecimalValue.Value = intVal1;

                    //DecimalValue.Value = (intVal1==null)?DBNull.Value:intVal1 ;
                    command.Parameters.Add(DecimalValue);

                    SqlParameter IdProduct = new SqlParameter("IdProduct", SqlDbType.Decimal);
                    IdProduct.Value = id;
                    command.Parameters.Add(IdProduct);
                    command.ExecuteNonQuery();

                    SqlCommand command1 = new SqlCommand("INSERT INTO ProductCharacteristics (idProduct, idCharacteristic, stringValue, decimalValue) VALUES(@IdProduct, 2, @StringValue, @DecimalValue)", connection);

                    SqlParameter StringValue1 = new SqlParameter("StringValue", SqlDbType.NVarChar);
                    StringValue1.IsNullable = true;
                    StringValue1.Value = strVal2;
                    command1.Parameters.Add(StringValue1);
                    SqlParameter DecimalValue1 = new SqlParameter("DecimalValue", SqlDbType.Decimal);
                    DecimalValue1.IsNullable = true;
                    if (intVal2 == null)
                        DecimalValue1.Value = DBNull.Value;
                    else
                        DecimalValue1.Value = intVal2;
                    command1.Parameters.Add(DecimalValue1);
                    SqlParameter IdProduct1 = new SqlParameter("IdProduct", SqlDbType.Decimal);
                    IdProduct1.Value = id;
                    command1.Parameters.Add(IdProduct1);
                    command1.ExecuteNonQuery();

                    SqlCommand command2 = new SqlCommand("INSERT INTO ProductCharacteristics (idProduct, idCharacteristic, stringValue, decimalValue) VALUES(@IdProduct, 3, @StringValue, @DecimalValue)", connection);

                    SqlParameter StringValue2 = new SqlParameter("StringValue", SqlDbType.NVarChar);
                    StringValue2.IsNullable = true;
                    StringValue2.Value = strVal3;
                    command2.Parameters.Add(StringValue2);
                    SqlParameter DecimalValue2 = new SqlParameter("DecimalValue", SqlDbType.Decimal);
                    DecimalValue2.IsNullable = true;
                    if (intVal3 == null)
                        DecimalValue2.Value = DBNull.Value;
                    else
                        DecimalValue2.Value = intVal3;
                    command2.Parameters.Add(DecimalValue2);
                    SqlParameter IdProduct2 = new SqlParameter("IdProduct", SqlDbType.Decimal);
                    IdProduct2.Value = id;
                    command2.Parameters.Add(IdProduct2);
                    

                    command2.ExecuteNonQuery();




                    connection.Close();
                }

            }
            catch (Exception ex)
            {
            }


        }

        public static double OrderPageCount(int orderStatus=0, string startDate=null, string endDate=null)
        {
            int pageSize = 10;
            int count = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();
                    // Работа с базой данных
                    SqlCommand command = null;
                    if (orderStatus != 0 && (startDate == null && endDate == null))
                    {
                        command = new SqlCommand("SELECT count(*) FROM OrdersList as o WHERE o.idStatus = @Status", connection);
                        SqlParameter Status = new SqlParameter("Status", SqlDbType.Int);
                        Status.Value = orderStatus;
                        command.Parameters.Add(Status);
                    }
                    else if (orderStatus == 0 && startDate != null && endDate != null)
                    {
                        string[] start = startDate.Split('.');
                        startDate = start[2] + "." + start[1] + "." + start[0];
                        string[] end = endDate.Split('.');
                        endDate = end[2] + "." + end[1] + "." + end[0];

                        command = new SqlCommand("SELECT count(*) FROM OrdersList as o WHERE o.OrderDate BETWEEN @StartDate AND @EndDate", connection);

                        SqlParameter StartDate = new SqlParameter("StartDate", SqlDbType.NVarChar);
                        StartDate.Value = startDate;
                        command.Parameters.Add(StartDate);

                        SqlParameter EndDate = new SqlParameter("EndDate", SqlDbType.NVarChar);
                        EndDate.Value = endDate;
                        command.Parameters.Add(EndDate);
                    }
                    else if (orderStatus != 0 && startDate != null && endDate != null)
                    {
                        string[] start = startDate.Split('.');
                        startDate = start[2] + "." + start[1] + "." + start[0];
                        string[] end = endDate.Split('.');
                        endDate = end[2] + "." + end[1] + "." + end[0];

                        command = new SqlCommand("SELECT count(*) FROM OrdersList as o WHERE o.idStatus = @Status AND o.OrderDate BETWEEN @StartDate AND @EndDate", connection);

                        SqlParameter StartDate = new SqlParameter("StartDate", SqlDbType.NVarChar);
                        StartDate.Value = startDate;
                        command.Parameters.Add(StartDate);

                        SqlParameter EndDate = new SqlParameter("EndDate", SqlDbType.NVarChar);
                        EndDate.Value = endDate;
                        command.Parameters.Add(EndDate);

                        SqlParameter Status = new SqlParameter("Status", SqlDbType.Int);
                        Status.Value = orderStatus;
                        command.Parameters.Add(Status);
                    }
                    else
                    {
                        command = new SqlCommand("SELECT count(*) FROM OrdersList", connection);
                    }
                    
                    count = (int)command.ExecuteScalar();
                    connection.Close();
                   

                }
            }
            catch (Exception ex)
            {
            }
            double val = (float)count / (float)pageSize;
            return Math.Ceiling(val);
        }

        public static double EditPageCount(int categoryFiltr=0, string activeFiltr=null)
        {
            int pageSize = 6;
            int count = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();
                    // Работа с базой данных
                     SqlCommand command =null;
                    if(categoryFiltr!= 0 && activeFiltr==null){
                        command = new SqlCommand("SELECT count(*) FROM Product WHERE Product.idСategory = @CategoryFiltr", connection);

                        SqlParameter CategoryFiltr = new SqlParameter("CategoryFiltr", SqlDbType.Int);
                        CategoryFiltr.Value = categoryFiltr;
                        command.Parameters.Add(CategoryFiltr);

                    }else if(categoryFiltr==0 && activeFiltr != null){
                        command = new SqlCommand("SELECT count(*) FROM Product WHERE Product.active = @ActiveFiltr", connection);
                        int activeFiltrInt = (activeFiltr == "true") ? 1 : 0;

                        SqlParameter ActiveFiltr = new SqlParameter("ActiveFiltr", SqlDbType.Bit);
                        ActiveFiltr.Value = activeFiltrInt;
                        command.Parameters.Add(ActiveFiltr);
                    }
                    else if (categoryFiltr != 0 && activeFiltr != null)
                    {
                        command = new SqlCommand("SELECT count(*) FROM Product WHERE Product.idСategory = @CategoryFiltr AND Product.active = @ActiveFiltr", connection);
                        int activeFiltrInt = (activeFiltr == "true") ? 1 : 0;

                        SqlParameter ActiveFiltr = new SqlParameter("ActiveFiltr", SqlDbType.Bit);
                        ActiveFiltr.Value = activeFiltrInt;
                        command.Parameters.Add(ActiveFiltr);

                        SqlParameter CategoryFiltr = new SqlParameter("CategoryFiltr", SqlDbType.Int);
                        CategoryFiltr.Value = categoryFiltr;
                        command.Parameters.Add(CategoryFiltr);

                    }else{
                        command = new SqlCommand("SELECT count(*) FROM Product", connection);
                    }

                    
                    count = (int)command.ExecuteScalar();
                    connection.Close();


                }
            }
            catch (Exception ex)
            {
            }
            double val = (float)count / (float)pageSize;
            return Math.Ceiling(val);
        } 
        
        public static List<OrderList> getUserOrders(string idUser){
            List<OrderList> userOrders = new  List<OrderList>();
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();
                    // Работа с базой данных
                    SqlCommand command = new SqlCommand("SELECT o.id, o.OrderDate, s.StatusName, o.Price FROM OrdersList as o, Status as s WHERE o.idStatus = s.id AND o.idUser = @IdUser", connection);
                    
                    SqlParameter IdUser = new SqlParameter("IdUser", SqlDbType.NVarChar);
                    IdUser.Value = idUser;
                    command.Parameters.Add(IdUser);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        OrderList obj = new OrderList();
                        obj.Id = (int)reader[0];
                        obj.Orderdate = (string)reader[1];
                        obj.Status = (string)reader[2];
                        obj.Price = (decimal)reader[3];
                        userOrders.Add(obj);
                    }
                    reader.Close();

                             
                    foreach (var obj in userOrders)
                    {
                        SqlCommand command1 = new SqlCommand("SELECT p.image, p.name, p.price,  b.count FROM Basket as b, Product as p WHERE b.orderNumber = @IdOrder AND b.idProduct = p.id ", connection);
          
                        SqlParameter IdOrder = new SqlParameter("IdOrder", SqlDbType.Int);
                        IdOrder.Value = obj.Id;
                        command1.Parameters.Add(IdOrder);

                        SqlDataReader reader1 = command1.ExecuteReader();
                        while (reader1.Read())
                        {
                            Description dobj = new Description();
                            dobj.image = (string)reader1[0];
                            dobj.name = (string)reader1[1];
                            dobj.price = (decimal)reader1[2];
                            dobj.count = (int)reader1[3];

                            obj.Descriptions.Add(dobj);

                        }
                        reader1.Close();
                    
                    }


                   
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return userOrders;




        }

        public static userModel getUser(string id)
        {
            userModel userInfo = new userModel();
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();
                    // Работа с базой данных
                    SqlCommand command = new SqlCommand("SELECT Id, Email, PasswordHash, PhoneNumber, UserName, Name, FirstName, Adress FROM AspNetUsers WHERE Id =@Id;", connection);
                    
                    SqlParameter Id = new SqlParameter("Id", SqlDbType.NVarChar);
                    Id.Value = id;
                    command.Parameters.Add(Id);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        userInfo.Id = (string)reader[0];
                        userInfo.Email = (string)reader[1];
                        userInfo.PasswordHash = (string)reader[2];
                        userInfo.PhoneNumber = (string)reader[3];
                        userInfo.UserName = (string)reader[4];
                        userInfo.Name = (string)reader[5];
                        userInfo.FirstName = (string)reader[6];
                        userInfo.Adress = (string)reader[7];
                       
                    }
                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return userInfo;

        }

        public static void editUser(string id, string email, string tel, string name, string firstName, string adress)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();
                    // Работа с базой данных

                    SqlCommand command = new SqlCommand("UPDATE AspNetUsers SET Email=@Email, PhoneNumber=@Tel, UserName=@Email, Name=@Name, FirstName=@FirstName, Adress=@Adress WHERE Id= @Id", connection);

                    SqlParameter Email = new SqlParameter("Email", SqlDbType.NVarChar);
                    Email.Value = email;
                    command.Parameters.Add(Email);

                    SqlParameter Tel = new SqlParameter("Tel", SqlDbType.NVarChar);
                    Tel.Value = tel;
                    command.Parameters.Add(Tel);

                    SqlParameter Name = new SqlParameter("Name", SqlDbType.NVarChar);
                    Name.Value = name;
                    command.Parameters.Add(Name);

                    SqlParameter FirstName = new SqlParameter("FirstName", SqlDbType.NVarChar);
                    FirstName.Value = firstName;
                    command.Parameters.Add(FirstName);

                    SqlParameter Adress = new SqlParameter("Adress", SqlDbType.NVarChar);
                    Adress.Value = adress;
                    command.Parameters.Add(Adress);

                    SqlParameter Id = new SqlParameter("Id", SqlDbType.NVarChar);
                    Id.Value = id;
                    command.Parameters.Add(Id);

                  


                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static string getUserEmail(string id)
        {
            string Email="";
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = settings.ConnectionString;
                    connection.Open();
                    // Работа с базой данных

                    SqlCommand command = new SqlCommand("SELECT Email FROM AspNetUsers WHERE Id= @Id", connection);
                                        
                    SqlParameter Id = new SqlParameter("Id", SqlDbType.NVarChar);
                    Id.Value = id;
                    command.Parameters.Add(Id);
                    
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Email = (string)reader[0];
                    }
                    reader.Close();
                    connection.Close();
                    
                }
            }
            catch (Exception ex)
            {
            }

            return Email;
        }

       
    }

}