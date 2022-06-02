# icd0006-2020s

Siim Liinat  
siliin  
193436

Exam - Quiz.

~~~
cd Exam
dotnet ef migrations --project DAL.App.EF --startup-project WebApp add Initial
dotnet ef database --project DAL.App.EF --startup-project WebApp update
~~~

Controllers + views
~~~
dotnet aspnet-codegenerator controller -name AnswerController -actions -m  Domain.App.Answer -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name QuestionController -actions -m  Domain.App.Question -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name QuizController -actions -m  Domain.App.Quiz -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name QuizzerController -actions -m  Domain.App.Quizzer -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name QuizzerAnswerController -actions -m  Domain.App.QuizzerAnswer -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name UsersController -actions -m  AppUser -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name RolesController -actions -m  AppRole -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
~~~

Scaffold Custom UI
~~~
cd WebApp
dotnet aspnet-codegenerator identity -dc DAL.App.EF.AppDbContext -f
~~~

ApiControllers
~~~
cd WebApp
dotnet aspnet-codegenerator controller -name AnswersController -actions -m  Domain.App.Answer -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name QuestionsController -actions -m  Domain.App.Question -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name QuizzesController -actions -m  Domain.App.Quiz -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name QuizzersController -actions -m  Domain.App.Quizzer -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name QuizzerAnswersController -actions -m  Domain.App.QuizzerAnswer -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f

dotnet aspnet-codegenerator controller -name AppRolesController -actions -m  Domain.App.Identity.AppRole -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name AppUserRolesController -actions -m  Domain.App.Identity.AppUserRole -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name AppUsersController -actions -m  Domain.App.Identity.AppUser -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
~~~