use smaDataBase
go
delete from languages
delete from variables
delete  from accessLevels

declare @en as varchar(50)
set @en=newid()
declare @ge  varchar(50)
set @ge =newid()
declare @ru  varchar(50)
set @ru=newid()
insert into languages(languageGUID,languageName)
values(@en,N'English'),(@ge,N'ქართული'),(@ru,N'русский')

insert into variables(languageGUID,variableGUID,variableName,value)

values  (@en,newid(),'home','Home'),
		(@en,newid(),'about','About'),
		(@en,newid(),'contact','Contact'),
		(@en,newid(),'login','Log In'),
		(@en,newid(),'register','Register'),
		(@ge,newid(),'home',N'სახლი'),
		(@ge,newid(),'about',N'ჩვენს შესახებ'),
		(@ge,newid(),'contact',N'საკონტაქტო ინფორმაცია'),
		(@ge,newid(),'login',N'სისტემაში შესვლა'),
		(@ge,newid(),'register',N'რეგისტრაცია')
go
declare @counter as integer
set @counter=1
insert into accessLevels(levelGUID,levelName,[level])
	values(newid(),'visitor',@counter)
	set @counter=@counter+1
insert into accessLevels(levelGUID,levelName,[level])
	values(newid(),'client',@counter)
	set @counter=@counter+1
insert into accessLevels(levelGUID,levelName,[level])
	values(newid(),'company_worker',@counter)
	set @counter=@counter+1
insert into accessLevels(levelGUID,levelName,[level])
	values(newid(),'company_HR',@counter)
	set @counter=@counter+1
insert into accessLevels(levelGUID,levelName,[level])
	values(newid(),'company_manager',@counter)
	set @counter=@counter+1
insert into accessLevels(levelGUID,levelName,[level])
	values(newid(),'ozzle_administration',@counter)
	set @counter=@counter+1
insert into accessLevels(levelGUID,levelName,[level])
	values(newid(),'ozzle_manager',@counter)
	set @counter=@counter+1

go

select * from accessLevels