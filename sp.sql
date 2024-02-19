/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50505
Source Host           : localhost:3306
Source Database       : sp

Target Server Type    : MYSQL
Target Server Version : 50505
File Encoding         : 65001

Date: 2024-02-16 11:40:47
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for productos
-- ----------------------------
DROP TABLE IF EXISTS `productos`;
CREATE TABLE `productos` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(20) NOT NULL,
  `estado` varchar(20) NOT NULL DEFAULT 'disponible',
  `precio` float NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- ----------------------------
-- Records of productos
-- ----------------------------
INSERT INTO `productos` VALUES ('1', 'Producto A', 'disponible', '8');
INSERT INTO `productos` VALUES ('2', 'Producto B', 'disponible', '1.5');
INSERT INTO `productos` VALUES ('3', 'Producto C', 'agotado', '80');

-- ----------------------------
-- Procedure structure for contarProductosPorEstado
-- ----------------------------
DROP PROCEDURE IF EXISTS `contarProductosPorEstado`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `contarProductosPorEstado`(
    IN nombre_estado VARCHAR(25),
    OUT numero INT)
BEGIN
    SELECT count (id) 
    INTO numero
    FROM productos
    WHERE estado = nombre_estado;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for obtenerProductos
-- ----------------------------
DROP PROCEDURE IF EXISTS `obtenerProductos`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `obtenerProductos`()
BEGIN
    SELECT * 
    FROM productos
    WHERE estado = 'disponible';

	SELECT * 
    FROM productos
    WHERE estado = 'agotado';
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for obtenerProductosPorEstado
-- ----------------------------
DROP PROCEDURE IF EXISTS `obtenerProductosPorEstado`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `obtenerProductosPorEstado`(IN nombre_estado VARCHAR(255))
BEGIN
    SELECT * 
    FROM productos
    WHERE estado = nombre_estado;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for venderProducto
-- ----------------------------
DROP PROCEDURE IF EXISTS `venderProducto`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `venderProducto`(
    INOUT beneficio INT(255),
    IN id_producto INT)
BEGIN
    SELECT @incremento_precio = precio 
    FROM productos
    WHERE id = id_producto;
    SET beneficio = beneficio + @incremento_precio;
END
;;
DELIMITER ;
