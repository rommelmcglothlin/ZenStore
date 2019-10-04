-- CREATE TABLE products
-- (
--   id VARCHAR(255) NOT NULL,
--   name VARCHAR(255) NOT NULL,
--   description VARCHAR(255),
--   price decimal(5,2) DEFAULT 4.99,

--   PRIMARY KEY(id)
-- );

-- CREATE TABLE reviews
-- (
--   productid VARCHAR(255) NOT NULL,
--   rating double(2,1) DEFAULT 0,
--   id VARCHAR(255) NOT NULL,
--   name VARCHAR(255),
--   description VARCHAR(255),

--   FOREIGN KEY (productid)
--   REFERENCES products(id),
--   PRIMARY KEY(id)
-- );

-- CREATE TABLE orders
-- (
--   id VARCHAR(255) NOT NULL,
--   name VARCHAR(255) NOT NULL,
--   canceled TINYINT DEFAULT 0,
--   shipped TINYINT DEFAULT 0,
--   orderplaced DATETIME, NOT NULL
--   shippeddate DATETIME,

--   PRIMARY KEY(id)
-- );

-- CREATE TABLE productorders
-- (
--   id VARCHAR(255) NOT NULL,
--   orderid VARCHAR(255) NOT NULL,
--   productid VARCHAR(255) NOT NULL,

--   FOREIGN KEY (orderid)
--   REFERENCES orders(id),
--   FOREIGN KEY (productid)
--   REFERENCES products(id),
--   PRIMARY KEY (id)
-- );

-- SELECT
-- o.id "order id",
-- o.name "customer name",
-- p.name "product name",
-- p.price,
-- o.orderplaced,
-- o.shippeddate,
-- o.canceled
-- FROM orders o