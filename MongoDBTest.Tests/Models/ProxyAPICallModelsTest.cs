using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDBTest.Models;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Diagnostics;

namespace MongoDBTest.Tests.Models
{
    [TestClass]
    public class ProxyAPICallModelsTest
    {
        /// <summary>
        /// (asnyc無法執行UnitTest)
        /// </summary>
        [TestMethod]
        public void Creat()
        {
            ProxyAPICallModels pac = new ProxyAPICallModels();
            pac.API_Name = "GetGamePoints";
            pac.Parameters = "{\"GameCodeInfo\":[\"C6B17C21-3751-4736-92DD-982CFCF96E1C\",\"4A287D31-3407-44E2-AED6-7E4EEE31B64C\",\"A9797B1B-439A-45C2-AB3E-57CC311D964B\",\"B821990C-2A1F-426D-85C3-390B0B16AF00\",\"8C0F981D-859F-4BAF-B9B6-947DC6D72AD7\"],\"UserId\":\"gotesta\",\"ExecUser\":\"SystemAdmin\",\"ExecIP\":\"192.168.1.1\"}";
            pac.API_CName = "用戶批量獲取遊戲端點數(額度)API";
            pac.ReturnCode = "200";
            pac.ReturnData = "{\"Code\":\"200\",\"ErrMsg\":\"\",\"RetObj\":[{\"GameGUID\":\"C6B17C21-3751-4736-92DD-982CFCF96E1C\",\"Point\":0.00,\"GetTime\":\"2015-10-01 01:37:35\",\"Success\":true},{\"GameGUID\":\"A9797B1B-439A-45C2-AB3E-57CC311D964B\",\"Point\":0.00,\"GetTime\":\"2015-10-01 01:37:34\",\"Success\":true},{\"GameGUID\":\"4A287D31-3407-44E2-AED6-7E4EEE31B64C\",\"Point\":0.00,\"GetTime\":\"2015-10-01 01:37:35\",\"Success\":true},{\"GameGUID\":\"B821990C-2A1F-426D-85C3-390B0B16AF00\",\"Point\":0.00,\"GetTime\":\"2015-10-01 01:37:35\",\"Success\":true},{\"GameGUID\":\"8C0F981D-859F-4BAF-B9B6-947DC6D72AD7\",\"Point\":1381.19,\"GetTime\":\"2015-10-01 01:37:35\",\"Success\":true}]}";
            pac.Description = "正常";
            pac.ExecuteUser = "SystemAdmin";

            // Act
            object result = pac.Creat();

            // Assert
            Assert.AreEqual(result, "0");
        }

        [TestMethod]
        public void GetOne()
        {
            ProxyAPICallModels pac = new ProxyAPICallModels();

            // Act
            object result = pac.GetOne("560d002482b1591aa849ff6e");

            // Assert
            Assert.AreEqual(result, "0");
        }

        [TestMethod]
        public void GetList()
        {
            List<ProxyAPICallModels> list = new List<ProxyAPICallModels>();

            // Act
            //object result = ProxyAPICallModels.GetList("GetGamePoints", "", "", out list);
            object result = ProxyAPICallModels.GetList("", "", "nick", out list);

            // Assert
            Assert.AreEqual(result, "0");
        }

        [TestMethod]
        public void GetAll()
        {
            List<ProxyAPICallModels> list = new List<ProxyAPICallModels>();

            // Act
            object result = ProxyAPICallModels.GetAll(out list);

            // Assert
            Assert.AreEqual(result, "0");
        }
    }
}
