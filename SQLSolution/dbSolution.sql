-----------------1---------------------------------
SELECT * FROM [dbo].[users] 
WHERE ID IN (3,2,4)         
-----------------1---------------------------------          

-----------------2---------------------------------
SELECT	usr.first_name,
		usr.last_name,
		(SELECT Count(status) FROM [dbo].[listings] WHERE status = 2 and user_id = usr.id) AS Basic,
		(SELECT Count(status) FROM [dbo].[listings] WHERE status = 3 and user_id = usr.id) AS Premium
FROM [dbo].[users] usr JOIN [dbo].[listings] lst
ON usr.Id = lst.user_id
WHERE usr.status = 2
GROUP BY usr.first_name,usr.last_name,usr.id    
-----------------2---------------------------------  

-----------------3---------------------------------
SELECT	usr.first_name,
		usr.last_name,
		(SELECT Count(status) FROM [dbo].[listings] WHERE status = 2 and user_id = usr.id) AS Basic,
		(SELECT Count(status) FROM [dbo].[listings] WHERE status = 3 and user_id = usr.id) AS Premium
FROM [dbo].[users] usr JOIN [dbo].[listings] lst
ON usr.Id = lst.user_id
WHERE usr.status = 2
GROUP BY usr.first_name,usr.last_name,usr.id    
HAVING (SELECT Count(status) FROM [dbo].[listings] WHERE status = 3 and user_id = usr.id) >= 1
-----------------3---------------------------------        

-----------------4---------------------------------
SELECT usr.first_name,
		usr.last_name,
		clk.currency,
		SUM(clk.price) AS Revenue
FROM [dbo].[users] usr LEFT JOIN [dbo].[listings] lst
ON usr.Id = lst.user_id LEFT JOIN [dbo].[clicks] clk
ON lst.id = clk.listing_id
WHERE usr.status = 2 AND clk.created > '2013-01-01'
GROUP BY usr.first_name,usr.last_name,clk.currency
-----------------4---------------------------------         

-----------------5---------------------------------  
INSERT INTO [dbo].[clicks] VALUES 
(3,4.00,'USD',getdate())
SELECT SCOPE_IDENTITY() AS Id
-----------------5---------------------------------        

-----------------6---------------------------------  

SELECT Name from [dbo].[listings] WHERE id NOT IN 
(SELECT DISTINCT lst.id
FROM [dbo].[listings] lst LEFT JOIN [dbo].[clicks] clk
ON lst.id = clk.listing_id
WHERE clk.created > '2013-01-01' AND clk.created < '2014-01-01')

-----------------6---------------------------------    

-----------------7---------------------------------   
SELECT	YEAR(clk.created) AS Year,
		Count(DISTINCT lst.id) AS total_listings_clicked,
		Count(DISTINCT usr.id) AS total_vendors_affected
FROM  [dbo].[clicks] clk LEFT JOIN [dbo].[listings] lst
ON clk.listing_id = lst.Id LEFT JOIN [dbo].[users] usr
ON lst.user_id = usr.Id 
GROUP BY YEAR(clk.created)
-----------------7---------------------------------  

-----------------8---------------------------------  
SELECT	usr.first_name,
		usr.last_name,
		STUFF((SELECT  ',' + lst.name
            FROM [dbo].[listings] lst
			WHERE lst.user_id = usr.Id 
        FOR XML PATH('')), 1, 1, '') AS listing_names
FROM [dbo].[users] usr
WHERE usr.status = 2
GROUP BY usr.first_name,usr.last_name,usr.Id
-----------------8---------------------------------  


