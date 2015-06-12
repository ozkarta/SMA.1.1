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

		(@en,newid(),'default_language','Default Language'),
		(@en,newid(),'registration_type','Registration Type'),
		(@en,newid(),'registration_user_name','User Name'),
		(@en,newid(),'registration_first_name','First Name'),
		(@en,newid(),'registration_last_name','Last Name'),
		(@en,newid(),'registration_phone_number','Contact Phone Number'),
		(@en,newid(),'registration_email','EMAIL'),
		(@en,newid(),'registration_passport_id','Passport Document ID'),
		(@en,newid(),'registration_password','Password'),
		(@en,newid(),'registration_confirm_password','Confirm Password'),
		(@en,newid(),'register_button','Register'),
		(@en,newid(),'register_window_title','Sign Up  On   '),
		(@en,newid(),'register_page_title','Registration On Ozzle'),
		(@en,newid(),'log_in_window_title','Log In  '),
		(@en,newid(),'log_in_user_name','User Name'),
		(@en,newid(),'log_in_password','Password'),
		(@en,newid(),'log_in_button','Log In'),
		(@en,newid(),'log_in_page_title','Log In  Ozzle'),

		(@ge,newid(),'home',N'სახლი'),
		(@ge,newid(),'about',N'ჩვენს შესახებ'),
		(@ge,newid(),'contact',N'საკონტაქტო ინფორმაცია'),
		(@ge,newid(),'login',N'სისტემაში შესვლა'),
		(@ge,newid(),'register',N'რეგისტრაცია'),

		(@ge,newid(),'default_language',N'ვებ საიტის ენა'),
		(@ge,newid(),'registration_type',N'რეგისტრაციის ტიპი'),
		(@ge,newid(),'registration_user_name',N'მომხმარებლის  ნიკი'),
		(@ge,newid(),'registration_first_name',N'სახელი'),
		(@ge,newid(),'registration_last_name',N'გვარი'),
		(@ge,newid(),'registration_phone_number',N'საკონტაკტო ტელეფონი'),
		(@ge,newid(),'registration_email',N'ელეკტრონული ფოსტა'),
		(@ge,newid(),'registration_passport_id',N'პირადი ნომერი'),
		(@ge,newid(),'registration_password',N'პაროლი'),
		(@ge,newid(),'registration_confirm_password',N'დაადასტურეთ პაროლი'),
		(@ge,newid(),'register_button',N'რეგისტრაცია'),
		(@ge,newid(),'register_window_title',N'რეგისტრაციის ფორმა  '),
		(@ge,newid(),'register_page_title',N'რეგისტრაცია   Ozzle - ზე'),
		(@ge,newid(),'log_in_window_title',N'ავტორიზაცია'),
		(@ge,newid(),'log_in_user_name',N'მომხმარებლის სახელი'),
		(@ge,newid(),'log_in_password',N'პაროლი'),
		(@ge,newid(),'log_in_button',N'სისტემაში შესვლა'),
		(@ge,newid(),'log_in_page_title',N'ავტორიზაცია  Ozzle - ზე')
		
go
declare @counter as integer
set @counter=1
insert into accessLevels(levelGUID,levelName,[level])
	values(newid(),'visitor',@counter)
	set @counter=@counter+1
insert into accessLevels(levelGUID,levelName,[level])
	values(newid(),'Client',@counter)
	set @counter=@counter+1
insert into accessLevels(levelGUID,levelName,[level])
	values(newid(),'CompanyWorker',@counter)
	set @counter=@counter+1
insert into accessLevels(levelGUID,levelName,[level])
	values(newid(),'CompanyHR',@counter)
	set @counter=@counter+1
insert into accessLevels(levelGUID,levelName,[level])
	values(newid(),'CompanyManager',@counter)
	set @counter=@counter+1
insert into accessLevels(levelGUID,levelName,[level])
	values(newid(),'SMAAdministrator',@counter)
	set @counter=@counter+1
insert into accessLevels(levelGUID,levelName,[level])
	values(newid(),'SMAManager',@counter)
	set @counter=@counter+1

go

select * from accessLevels