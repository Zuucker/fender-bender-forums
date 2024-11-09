-- Inserting into "AspNetUsers" table
INSERT INTO "AspNetUsers" ("Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount")
VALUES 
('user1', 'user1', 'USER1', 'user1@example.com', 'USER1@EXAMPLE.COM', TRUE, 'hashedpassword1', 'securitystamp1', 'concurrencystamp1', '1234567890', TRUE, FALSE, NULL, TRUE, 0),
('user2', 'user2', 'USER2', 'user2@example.com', 'USER2@EXAMPLE.COM', TRUE, 'hashedpassword2', 'securitystamp2', 'concurrencystamp2', '2345678901', TRUE, FALSE, NULL, TRUE, 0),
('user3', 'user3', 'USER3', 'user3@example.com', 'USER3@EXAMPLE.COM', TRUE, 'hashedpassword3', 'securitystamp3', 'concurrencystamp3', '3456789012', TRUE, FALSE, NULL, TRUE, 0),
('user4', 'user4', 'USER4', 'user4@example.com', 'USER4@EXAMPLE.COM', TRUE, 'hashedpassword4', 'securitystamp4', 'concurrencystamp4', '4567890123', TRUE, FALSE, NULL, TRUE, 0),
('user5', 'user5', 'USER5', 'user5@example.com', 'USER5@EXAMPLE.COM', TRUE, 'hashedpassword5', 'securitystamp5', 'concurrencystamp5', '5678901234', TRUE, FALSE, NULL, TRUE, 0);


-- Inserting into "Sections" table
INSERT INTO "Sections" ("SectionId", "Name", "Description")
VALUES 
(1, 'Section 1', 'Description 1'),
(2, 'Section 2', 'Description 2'),
(3, 'Section 3', 'Description 3'),
(4, 'Section 4', 'Description 4'),
(5, 'Section 5', 'Description 5');


-- Inserting into "Posts" table
INSERT INTO "Posts" ("Id", "AuthorId", "SectionId", "CreationDate", "Content", "AdditionalContentId", "Title", "Tags", "UserId")
VALUES 
(1, 1, 1, '2024-08-11 00:00:00+00', 101, 1001, 'Post Title 1', 'tag1,tag2', 'user1'),
(2, 2, 1, '2024-08-11 01:00:00+00', 102, 1002, 'Post Title 2', 'tag3,tag4', 'user2'),
(3, 3, 2, '2024-08-11 02:00:00+00', 103, 1003, 'Post Title 3', 'tag5,tag6', 'user3'),
(4, 4, 2, '2024-08-11 03:00:00+00', 104, 1004, 'Post Title 4', 'tag7,tag8', 'user4'),
(5, 5, 3, '2024-08-11 04:00:00+00', 105, 1005, 'Post Title 5', 'tag9,tag10', 'user5');

-- Inserting into "AdditionalContents" table
INSERT INTO "AdditionalContents" ("AdditionalContentId", "Type", "Content", "Path")
VALUES 
(1001, 1, 201, 301),
(1002, 1, 202, 302),
(1003, 2, 203, 303),
(1004, 2, 204, 304),
(1005, 3, 205, 305);

-- Inserting into "Cities" table
INSERT INTO "Cities" ("CityId", "Altitude", "Latitude", "Name")
VALUES 
(1, 500.0, 45.0, 'City 1'),
(2, 600.0, 46.0, 'City 2'),
(3, 700.0, 47.0, 'City 3'),
(4, 800.0, 48.0, 'City 4'),
(5, 900.0, 49.0, 'City 5');

-- Inserting into "Cars" table
INSERT INTO "Cars" ("CarId", "Year", "Manufacturer", "Model", "Generation", "Type")
VALUES 
(1, '2010-01-01 00:00:00+00', 'Toyota', 'Camry', 'XLE', 'Sedan'),
(2, '2011-01-01 00:00:00+00', 'Honda', 'Accord', 'EX', 'Sedan'),
(3, '2012-01-01 00:00:00+00', 'Ford', 'Focus', 'Titanium', 'Hatchback'),
(4, '2013-01-01 00:00:00+00', 'Chevrolet', 'Malibu', 'LT', 'Sedan'),
(5, '2014-01-01 00:00:00+00', 'Nissan', 'Altima', 'SL', 'Sedan');


-- Inserting into "Offers" table
INSERT INTO public."Offers"(
    "Price", "CarId", "AuthorId", "Date", "Condition", "Fuel", "Color", "Mileage", "Tags", "AdditionalContentId", "OwnerId", "CityId")
VALUES 
    (10000.0, 1, 1, '2024-08-11 00:00:00+00', 1, 1, 'Red', 10000, 'tag1,tag2', 1001, 'user1', 1),
    (20000.0, 2, 2, '2024-08-11 01:00:00+00', 2, 2, 'Blue', 20000, 'tag3,tag4', 1002, 'user2', 2),
    (30000.0, 3, 3, '2024-08-11 02:00:00+00', 3, 3, 'Green', 30000, 'tag5,tag6', 1003, 'user3', 3),
    (40000.0, 4, 4, '2024-08-11 03:00:00+00', 4, 4, 'Yellow', 40000, 'tag7,tag8', 1004, 'user4', 4),
    (50000.0, 5, 5, '2024-08-11 04:00:00+00', 5, 5, 'Black', 50000, 'tag9,tag10', 1005, 'user5', 5);

-- Inserting into "OfferRates" table
INSERT INTO "OfferRates" ("OfferRateId", "UserId", "OfferId", "Score")
VALUES 
(1, 1, 1, 5),
(2, 2, 2, 4),
(3, 3, 3, 3),
(4, 4, 4, 2),
(5, 5, 5, 1);

-- Inserting into "Comments" table
INSERT INTO "Comments" ("CommentId", "Upvotes", "ParentId", "PostId", "OfferId", "Content")
VALUES 
(1, 10, NULL, 1, 1, 'Comment 1'),
(2, 20, 1, 2, 2, 'Comment 2'),
(3, 30, 2, 3, 3, 'Comment 3'),
(4, 40, 3, 4, 4, 'Comment 4'),
(5, 50, 4, 5, 5, 'Comment 5');

-- Inserting into "Likes" table
INSERT INTO "Likes" ("LikeId", "AuthorId", "OfferId", "PostId", "CommentId", "UserId")
VALUES 
(1, 1, 1, 1, 1, 'user1'),
(2, 2, 2, 2, 2, 'user2'),
(3, 3, 3, 3, 3, 'user3'),
(4, 4, 4, 4, 4, 'user4'),
(5, 5, 5, 5, 5, 'user5');
