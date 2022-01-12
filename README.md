# RAS-TP

Atençao
Sempre que alterarem os campos da BD têm que a remover e voltar a criar

$ dotnet ef database drop
  Removam a pasta Migrations
$ dotnet ef migrations add InitialCreate
$ dotnet ef update database
   

 ROLES
  Admin
  Moderator
  Member
   
 DB_EVENT_STATE (Id	Description)   -> Estado de um evento
  1	Open
  2	Finished
  3	Suspended
  
 DB_SPORTS (Id	Description	sportType_Id)
  1	Soccer	2
  2	Chess	1
  
 DB_SPORTS_TYPE (Id	Description)
  1	Individual
  2	Team
