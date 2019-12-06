using MySql.Data.MySqlClient;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Friedrich.Models
{
    public class DataHelper
    {
        private string conn = "Server=127.0.0.1;Port=3306;Database=friedrich;Uid=root;Pwd=root;";
        private MySqlConnection mscon;
        public DataHelper()
        {
            try
            {
                mscon = new MySqlConnection(conn);
                mscon.Open();
            }
            catch (Exception)
            {
                mscon.Close();
            }
        }
        public Car GetCar(String id)
        {
            var table = new DataTable();
            MySqlDataAdapter adp = new MySqlDataAdapter("SELECT * FROM cars WHERE id ='" + id + "';", mscon);
            adp.Fill(table);
            var tmp_images = new List<Car>();
            tmp_images = (from DataRow dr in table.Rows
                          select new Car()
                          {
                              id = dr["id"].ToString(),
                              brand_id = dr["brand_id"].ToString(),
                              year = Convert.ToInt32(dr["year"]),
                              mileAge = Convert.ToInt32(dr["mileage"]),
                              Fuel = dr["fuel"].ToString(),
                              available = Convert.ToBoolean(dr["available"]),
                              price = Convert.ToInt32(dr["price"]),
                              ImagesPath = GetImages(dr["id"].ToString()),
                              brand = GetBrand(dr["brand_id"].ToString())
                          }).ToList();

            return tmp_images[0];
        }
        public List<Car> GetCars()
        {
            var table = new DataTable();
            MySqlDataAdapter adp = new MySqlDataAdapter("SELECT * FROM cars;", mscon);
            adp.Fill(table);
            var tmp_images = new List<Car>();
            tmp_images = (from DataRow dr in table.Rows
                          select new Car()
                          {
                              id = dr["id"].ToString(),
                              brand_id = dr["brand_id"].ToString(),
                              year = Convert.ToInt32(dr["year"]),
                              mileAge = Convert.ToInt32(dr["mileage"]),
                              price = Convert.ToInt32(dr["price"]),
                              available = Convert.ToBoolean(dr["available"]),
                              Fuel = dr["fuel"].ToString(),
                              ImagesPath = GetImages(dr["id"].ToString()),
                              brand = GetBrand(dr["brand_id"].ToString())
                          }).ToList();

            return tmp_images;
        }
        public List<Image> GetImages (String carId)
        {
            var table = new DataTable();
            MySqlDataAdapter adp = new MySqlDataAdapter("SELECT * FROM images WHERE model_id = '" + carId + "';", mscon);
            adp.Fill(table);
            var tmp_images = new List<Image>();
            tmp_images = (from DataRow dr in table.Rows
                          select new Image()
                          {
                              id = dr["id"].ToString(),
                              model_id = dr["model_id"].ToString(),
                              path = dr["path"].ToString()
                          }).ToList();
            return tmp_images;
        }
        public Brand GetBrand(String brandId)
        {
            var table = new DataTable();
            MySqlDataAdapter adp = new MySqlDataAdapter("SELECT * FROM brands WHERE id = '" + brandId + "';", mscon);
            adp.Fill(table);
            var tmp_images = new List<Brand>();
            tmp_images = (from DataRow dr in table.Rows
                          select new Brand()
                          {
                              Id = dr["id"].ToString(),
                              Model = dr["model"].ToString(),
                              Company = dr["company"].ToString(),
                              Premium = Convert.ToBoolean(dr["premium"]),
                          }).ToList();
            return tmp_images.First();
        }
        public bool Contains(User user)
        {
            var table = new DataTable();
            MySqlDataAdapter adp = new MySqlDataAdapter("SELECT * FROM users WHERE login = '" +user.Login+ "';", mscon);
            adp.Fill(table);
            if (table.Rows.Count == 0) return false;
            if(user.Password == table.Rows[0]["password"].ToString())
            {
                return true;
            }
            return false;

        }
        public void UserPay(User user)
        {
            MySqlCommand command = new MySqlCommand("UPDATE users SET car_id = '"+ user.RentedCar_id+"' WHERE password = '"+user.Password+"' AND login ='"+user.Login+"';", mscon);
            command.ExecuteNonQuery();
            MySqlCommand command2 = new MySqlCommand("UPDATE cars SET available = false WHERE id = '"+ user.RentedCar_id + "';", mscon);
            command2.ExecuteNonQuery();
        }
        public bool AddUser(User user)
        {
            if (Contains(user))
            {
                return false;
            }
            MySqlCommand command = new MySqlCommand("INSERT INTO users (id, card_numder, login, password, name,last_name, license)" +
                " VALUES('" + user.Id+"','"+user.Card+"'," + "'"+user.Login+"','"+user.Password+"','"+user.Name+"','"+user.LastName+"','"+
                user.licenseNumber+"');", mscon);
            command.ExecuteNonQuery();
            return true;
        }
        public void Delete(String id)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM cars WHERE id = '" + id + "';" , mscon);
            command.ExecuteNonQuery();
        }
        public void UPDATE(Car car)
        {
            MySqlCommand command = new MySqlCommand("UPDATE cars SET year="+car.year+", mileage = "+car.mileAge+", price = "+car.price+", fuel ='"+car.Fuel+ "', available = "+car.available+" WHERE id ='"+car.id+"';", mscon);
            command.ExecuteNonQuery();
        }
        public void INSERT (Car car)
        {
            if(!ContainsBrand(car.brand))
            {
                MySqlCommand commandBrand = new MySqlCommand("INSERT INTO brands VALUES('"+car.brand.Id+"','"+car.brand.Model+"', '"+car.brand.Company+"', "+car.brand.Premium+");", mscon);
                commandBrand.ExecuteNonQuery();
                car.brand_id = car.brand.Id;
            }
            else
            {
                car.brand_id = GetBrandId(car.brand);
            }
            MySqlCommand command = new MySqlCommand("INSERT INTO cars VALUES('"+car.id+"','"+car.brand_id+"',"+car.year+","+car.mileAge+","+car.price+",'"+car.Fuel+"',"+car.available+");", mscon);
            command.ExecuteNonQuery();
            foreach(var image in car.ImagesPath)
            {
                image.model_id = car.id;
                MySqlCommand commandIMG = new MySqlCommand("INSERT INTO images VALUES('"+image.id+"','"+ image.model_id + "','"+ image.path + "')", mscon);
                commandIMG.ExecuteNonQuery();
            }
        }
        public bool ContainsBrand(Brand brand)
        {
            var table = new DataTable();
            MySqlDataAdapter adp = new MySqlDataAdapter("SELECT * FROM brands WHERE model = '" + brand.Model + "';", mscon);
            adp.Fill(table);
            if (table.Rows.Count == 0) return false;
            if (brand.Company == table.Rows[0]["company"].ToString())
            {
                return true;
            }
            return false;
        }
        public bool ContainsAdmin(Admin admin)
        {
            var table = new DataTable();
            MySqlDataAdapter adp = new MySqlDataAdapter("SELECT * FROM admins WHERE login = '" + admin.Login + "';", mscon);
            adp.Fill(table);
            if (table.Rows.Count == 0) return false;
            if (admin.Password == table.Rows[0]["password"].ToString())
            {
                return true;
            }
            return false;

        }
        public String GetBrandId(Brand brand)
        {
            var table = new DataTable();
            MySqlDataAdapter adp = new MySqlDataAdapter("SELECT * FROM brands WHERE model = '" + brand.Model + "';", mscon);
            adp.Fill(table);
            if (table.Rows.Count == 0) return "";
            if (brand.Company == table.Rows[0]["company"].ToString())
            {
                return table.Rows[0]["id"].ToString();
            }
            return "";
        }
    }
}