-- Fr�ga 1: H�mta alla produkter med deras namn, pris och kategorinamn. Sortera p� kategorinamn och sedan produktens namn.
SELECT p.ProductName, p.UnitPrice, c.CategoryName
FROM Products p
INNER JOIN Categories c ON p.CategoryID = c.CategoryID
ORDER BY c.CategoryName, p.ProductName;

-- Fr�ga 2: H�mta alla kunder och antalet ordrar de gjort. Sortera i fallande ordning efter antalet ordrar.
SELECT c.CustomerID, c.CompanyName, COUNT(o.OrderID) AS OrderCount
FROM Customers c
LEFT JOIN Orders o ON c.CustomerID = o.CustomerID
GROUP BY c.CustomerID, c.CompanyName
ORDER BY OrderCount DESC;

-- Fr�ga 3: H�mta alla anst�llda tillsammans med territorierna de har hand om (EmployeeTerritories och Territories tabellerna).
SELECT e.EmployeeID, e.FirstName, e.LastName, t.TerritoryDescription
FROM Employees e
INNER JOIN EmployeeTerritories et ON e.EmployeeID = et.EmployeeID
INNER JOIN Territories t ON et.TerritoryID = t.TerritoryID;