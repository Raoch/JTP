function blogCtrl(t,o){var e=this;e.pageVM={},e.blogs=[],e.selectedBlog={},e.searchText="",e.tagss=[""],e.testQuill,e.initBlogs=function(n){e.blogs=n;o.get("http://localhost:61143/BlogApi/GetBlogByID?id=1").then(function(o){t.value=o},function(t){alert(t)})},e.initBlogPost=function(t){e.selectedBlog=t;e.getSelectedBlog()},e.CreateBlogPostInit=function(t){e.pageVM=t;angular.element;n(),i()};var n=function(){$("#my-tag-list").tags({tagData:e.tags,suggestions:e.pageVM.Tags.map(t=>t.Name),excludeList:["not","these","words"]})},i=function(){e.quill=new Quill("#editor",{theme:"snow",bounds:"#scrolling-container",scrollingContainer:"#scrolling-container"})};e.choiceSet={choices:[]},e.addNewChoice=function(){e.choiceSet.choices.push(e.tagToAdd)},e.removeChoice=function(t){e.choiceSet.choices.splice(t,1)},e.getActiveBlogs=function(){},e.getSelectedBlog=function(){return o.get("http://localhost:61143/BlogApi/GetBlogByID?id=1").then(function(t){return t},function(t){alert(t)})},e.addNewBlogPost=function(){e.newBlogPostModel.Content=e.quill.container.innerHTML;new Headers;JSON.stringify(e.newBlogPostModel);o.post("http://localhost:61143/BlogApi/PostBlog",{Name:"bpToAdd",Json:JSON.stringify(e.newBlogPostModel)}).then(function(o){t.value=o},function(t){alert(t)})},e.postRequest=function(e){o.post(e).then(function(o){t.value=o},function(t){alert(t)})}}function homeCtrl(t){this.searchText="",this.Message="Congratulation you have created your first application using AngularJs",this.tags=["Politics","Philosophy","Bottom Up Systems","Putin","Stoicism","Brexit","Trump","History","Tech"]}!function(){var t=angular.module("MyApp",[]);t.controller("BlogCtrl",["$scope","$http","$q",blogCtrl]),t.controller("HomeCtrl",homeCtrl)}();