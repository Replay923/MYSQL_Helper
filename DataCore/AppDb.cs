using System;
using MySql.Data.MySqlClient;

namespace DataCore
{
    public class AppDb : IDisposable
    {
        public static void Initialize()
        {
            using (var db = new AppDb())
            {
                if (db.ClearData == false)
                    return;
                db.Connection.Open();
                var cmd = db.Connection.CreateCommand();
                cmd.CommandText = @"
/*
Navicat MySQL Data Transfer

Source Server         : Category
Source Server Version : 80012
Source Host           : 127.0.0.1:3306
Source Database       : categorydata

Target Server Type    : MYSQL
Target Server Version : 80012
File Encoding         : 65001

Date: 2018-09-18 17:36:21
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `media`
-- ----------------------------
DROP TABLE IF EXISTS `media`;
CREATE TABLE `media` (
  `id` char(140) COLLATE utf8_croatian_ci NOT NULL,
  `newsId` char(140) COLLATE utf8_croatian_ci NOT NULL,
  `index` int(10) COLLATE utf8_croatian_ci NOT NULL DEFAULT '0',
  `sourceUrl` varchar(500) COLLATE utf8_croatian_ci DEFAULT NULL,
  `type` smallint(1) DEFAULT '1',
  PRIMARY KEY (`id`),
  KEY `newsIdForeignKey` (`newsId`),
  CONSTRAINT `newsIdForeignKey` FOREIGN KEY (`newsId`) REFERENCES `newsitem` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_croatian_ci;

-- ----------------------------
-- Records of media
-- ----------------------------
SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `newsdetail`
-- ----------------------------
DROP TABLE IF EXISTS `newsdetail`;
CREATE TABLE `newsdetail` (
  `id` char(140) CHARACTER SET utf8 COLLATE utf8_croatian_ci NOT NULL,
  `title` varchar(500) CHARACTER SET utf8 COLLATE utf8_croatian_ci DEFAULT NULL,
  `author` varchar(100) CHARACTER SET utf8 COLLATE utf8_croatian_ci DEFAULT NULL,
  `contentText` text CHARACTER SET utf8 COLLATE utf8_croatian_ci,
  `contentHtml` text CHARACTER SET utf8 COLLATE utf8_croatian_ci,
  `existMedia` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  CONSTRAINT `idForeignKey` FOREIGN KEY (`id`) REFERENCES `newsitem` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_croatian_ci;

-- ----------------------------
-- Records of newsdetail
-- ----------------------------

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `newsitem`
-- ----------------------------
DROP TABLE IF EXISTS `newsitem`;
CREATE TABLE `newsitem` (
  `id` char(140) CHARACTER SET utf8 COLLATE utf8_croatian_ci NOT NULL,
  `pageIndex` int(10) NOT NULL,
  `typeHash` char(128) CHARACTER SET utf8 COLLATE utf8_croatian_ci NOT NULL,
  `time` datetime NOT NULL,
  `index` int(10) NOT NULL,
  `title` varchar(500) CHARACTER SET utf8 COLLATE utf8_croatian_ci DEFAULT NULL,
  `titleImg` varchar(500) CHARACTER SET utf8 COLLATE utf8_croatian_ci DEFAULT NULL,
  `linkUrl` varchar(500) CHARACTER SET utf8 COLLATE utf8_croatian_ci DEFAULT NULL,
  `desc` varchar(2000) CHARACTER SET utf8 COLLATE utf8_croatian_ci DEFAULT NULL,
  `author` varchar(100) CHARACTER SET utf8 COLLATE utf8_croatian_ci DEFAULT NULL,
  `isDone` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `typeForeignKey` (`typeHash`),
  CONSTRAINT `typeForeignKey` FOREIGN KEY (`typeHash`) REFERENCES `page` (`hash`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_croatian_ci;

-- ----------------------------
-- Records of newsitem
-- ----------------------------

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `page`
-- ----------------------------
DROP TABLE IF EXISTS `page`;
CREATE TABLE `page` (
  `hash` char(128) CHARACTER SET utf8 COLLATE utf8_croatian_ci NOT NULL,
  `appName` varchar(200) CHARACTER SET utf8 COLLATE utf8_croatian_ci DEFAULT NULL,
  `pageType` varchar(200) CHARACTER SET utf8 COLLATE utf8_croatian_ci DEFAULT NULL,
  PRIMARY KEY (`hash`),
  KEY `hash` (`hash`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_croatian_ci;

-- ----------------------------
-- Records of page
-- ----------------------------

			";
                cmd.ExecuteNonQuery();
            }
        }

        public MySqlConnection Connection;
        public bool ClearData = false;
        public static string[] WebPort;

        public AppDb()
        {
            Connection = new MySqlConnection(AppConfig.Config["Data:ConnectionString"]);
            string clear = AppConfig.Config["Data:ClearData"];
            ClearData = string.Equals("true", clear) ? true : false;
            WebPort = AppConfig.Config["Data:WebPort"].Split('|');
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}
