(function () {
    //Module
    var app = angular.module('MyApp', []);
    //Controller
    app.controller('BlogCtrl', ['$scope', blogCtrl]);
    app.controller('HomeCtrl', homeCtrl);

    //Services


})();

function blogCtrl($scope) {
    var bc = this;
    bc.blogs = [];
    bc.selectedBlog = {};
    bc.searchText = "";
    bc.tags = ["Politics", "Philosophy", "Bottom Up Systems", "Putin", "Stoicism", "Brexit", "Trump", "History", "Tech"];

    bc.initBlogs = function (blogs) {
        bc.blogs = blogs
        console.log("TEST WORKED!!");
    };

    bc.initBlogPost = function (blog) {
        bc.selectedBlog = blog;
        console.log("TEST 22 WORKED!!");
    };
}
function homeCtrl($scope) {
    var bc = this;
    bc.searchText = "";
    bc.Message = "Congratulation you have created your first application using AngularJs";
    bc.tags = ["Politics", "Philosophy", "Bottom Up Systems", "Putin", "Stoicism", "Brexit", "Trump", "History", "Tech"];
}