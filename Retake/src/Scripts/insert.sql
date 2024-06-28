INSERT INTO Department (DepName, DepLocation) VALUES
                                                  ('ACCOUNTING', 'NEW YORK'),
                                                  ('RESEARCH',   'DALLAS'),
                                                  ('SALES',      'CHICAGO'),
                                                  ('OPERATIONS', 'BOSTON');

INSERT INTO Employees (EmpName, JobName, ManagerID, HireDate, Salary, Commission, DepID) VALUES
                                                                                             ('SMITH',  'CLERK',     NULL, '1980-03-17',  800, NULL, (SELECT DepID FROM Department WHERE DepName = 'RESEARCH')),
                                                                                             ('ALLEN',  'SALESMAN',  NULL, '1981-03-20', 1600,  300, (SELECT DepID FROM Department WHERE DepName = 'SALES')),
                                                                                             ('WARD',   'SALESMAN',  NULL, '1981-03-22', 1250,  500, (SELECT DepID FROM Department WHERE DepName = 'SALES')),
                                                                                             ('JONES',  'MANAGER',   NULL, '1981-03-02', 2975, NULL, (SELECT DepID FROM Department WHERE DepName = 'RESEARCH')),
                                                                                             ('MARTIN', 'SALESMAN',  NULL, '1981-03-28', 1250, 1400, (SELECT DepID FROM Department WHERE DepName = 'SALES')),
                                                                                             ('BLAKE',  'MANAGER',   NULL, '1981-03-01', 2850, NULL, (SELECT DepID FROM Department WHERE DepName = 'SALES')),
                                                                                             ('CLARK',  'MANAGER',   NULL, '1981-03-09', 2450, NULL, (SELECT DepID FROM Department WHERE DepName = 'ACCOUNTING')),
                                                                                             ('SCOTT',  'ANALYST',   NULL, '1982-03-09', 3000, NULL, (SELECT DepID FROM Department WHERE DepName = 'RESEARCH')),
                                                                                             ('KING',   'PRESIDENT', NULL, '1981-03-17', 5000, NULL, (SELECT DepID FROM Department WHERE DepName = 'ACCOUNTING')),
                                                                                             ('TURNER', 'SALESMAN',  NULL, '1981-03-08', 1500, 100, (SELECT DepID FROM Department WHERE DepName = 'SALES')),
                                                                                             ('ADAMS',  'CLERK',     NULL, '1983-03-12', 1100, NULL, (SELECT DepID FROM Department WHERE DepName = 'RESEARCH')),
                                                                                             ('JAMES',  'CLERK',     NULL, '1981-03-03',  950, NULL, (SELECT DepID FROM Department WHERE DepName = 'SALES')),
                                                                                             ('FORD',   'ANALYST',   NULL, '1981-03-03', 3000, NULL, (SELECT DepID FROM Department WHERE DepName = 'RESEARCH')),
                                                                                             ('MILLER', 'CLERK',     NULL, '1982-03-23', 1300, NULL, (SELECT DepID FROM Department WHERE DepName = 'ACCOUNTING'));

UPDATE Employees
SET ManagerID = (SELECT EmpID from Employees WHERE EmpName = 'KING')
WHERE EmpName = 'JONES' OR EmpName = 'BLAKE' OR EmpName = 'CLARK' OR EmpName = 'SCOTT';

UPDATE Employees
SET ManagerID = (SELECT EmpID from Employees WHERE EmpName = 'BLAKE')
WHERE EmpName = 'MARTIN' OR EmpName = 'WARD' OR EmpName = 'ALLEN' OR EmpName = 'TURNER';

INSERT INTO SalaryGrade (MinSalary, MaxSalary) VALUES
                                                   (700, 1200),
                                                   (1201, 1400),
                                                   (1401, 2000),
                                                   (2001, 3000),
                                                   (3001, 9999);