-- Fråga 1: Hämta alla produkter med deras namn, pris och kategorinamn. Sortera på kategorinamn och sedan produktens namn.
SELECT p.ProductName, p.UnitPrice, c.CategoryName
FROM Products p
INNER JOIN Categories c ON p.CategoryID = c.CategoryID
ORDER BY c.CategoryName, p.ProductName;

-- Fråga 2: Hämta alla kunder och antalet ordrar de gjort. Sortera i fallande ordning efter antalet ordrar.
SELECT c.CustomerID, c.CompanyName, COUNT(o.OrderID) AS OrderCount
FROM Customers c
LEFT JOIN Orders o ON c.CustomerID = o.CustomerID
GROUP BY c.CustomerID, c.CompanyName
ORDER BY OrderCount DESC;

-- Fråga 3: Hämta alla anställda tillsammans med territorierna de har hand om (EmployeeTerritories och Territories tabellerna).
SELECT e.EmployeeID, e.FirstName, e.LastName, t.TerritoryDescription
FROM Employees e
INNER JOIN EmployeeTerritories et ON e.EmployeeID = et.EmployeeID
INNER JOIN Territories t ON et.TerritoryID = t.TerritoryID;