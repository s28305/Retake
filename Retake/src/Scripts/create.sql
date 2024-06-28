-- Create the department table
CREATE TABLE Department (
                            DepID INT PRIMARY KEY IDENTITY,
                            DepName VARCHAR(50) NOT NULL,
                            DepLocation VARCHAR(50) NOT NULL,
                            CONSTRAINT Department_uk UNIQUE (DepName)
);

-- Create the salary_grade table
CREATE TABLE SalaryGrade (
                             Grade INT PRIMARY KEY IDENTITY,
                             MinSalary INT NOT NULL,
                             MaxSalary INT NOT NULL
);

-- Create the employees table
CREATE TABLE Employees (
                           EmpID INT PRIMARY KEY IDENTITY,
                           EmpName VARCHAR(100) NOT NULL,
                           JobName VARCHAR(50) NOT NULL,
                           ManagerID INT,
                           HireDate DATE NOT NULL,
                           Salary DECIMAL(10, 2) NOT NULL,
                           Commission DECIMAL(7, 2),
                           DepID INT NOT NULL,
                           FOREIGN KEY (DepID) REFERENCES Department (DepID),
                           FOREIGN KEY (ManagerID) REFERENCES Employees (EmpID)
);
