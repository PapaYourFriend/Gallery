use Gallery;

GO

CREATE PROCEDURE Initial AS 
INSERT Admins(Login, Password)
VALUES ('admin', 'bQjHeMZn4HUqcd1aabQDwsIhLL5qptW3Lb8ftyBZrFOnDMf3')//aadmin
INSERT OrderStatus(OrderStatusName)
VALUES
('InProcessing'),
('Packaging'),
('OnTheWay'),
('Accepted'),
('Abandoned')