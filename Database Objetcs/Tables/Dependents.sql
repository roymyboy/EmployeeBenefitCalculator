Create table Dependents
(
	DependentID uniqueIdentifier primary key,
    EmployeeID uniqueIdentifier,
    FirstName varchar(255) Not Null,
    LastName varchar(255) Not null,
	Relationship varchar(255) not null
)

