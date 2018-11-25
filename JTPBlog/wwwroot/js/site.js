(function () {
    //Module
    var app = angular.module('MyApp', []);
    //Controller
    app.controller('BlogCtrl', ['$scope', '$window', '$http', '$q', blogCtrl]);
    app.controller('HomeCtrl', homeCtrl);

    //Services

})();

function blogCtrl($scope, $window, $http) {
    var bc = this;
    bc.pageVM = {};
    bc.blogs = [];
    bc.selectedBlog = {};
    bc.searchText = "";
    bc.tagss = [''];
    bc.testQuill;

    bc.initBlogs = function (blogs) {
        bc.blogs = blogs;
    };
    bc.initBlogPost = function (blog) {
        bc.selectedBlog = blog;
    };
    bc.initCreateBlogPost = function (pageViewModel) {
        bc.pageVM = pageViewModel;
        var $ = angular.element;
        setTagsUp();
        setQuillUp();   
        bc.choiceSet.choices;
    };

    var setTagsUp = function () {
        $('#my-tag-list').tags({
            tagData: bc.tags,
            suggestions: bc.pageVM.Tags.map(a => a.Name),
            excludeList: ["not", "these", "words"]
        });
    };
    var setQuillUp = function () {
        bc.quill = new Quill('#editor', {
            theme: 'snow',
            bounds: '#scrolling-container',
            scrollingContainer: '#scrolling-container'
        });
    }
    // tags
    bc.choiceSet = { choices: [] };
    bc.addNewChoice = function () {
        bc.choiceSet.choices.push({ "Name": bc.tagToAdd, "ID": "1" });

    };
    bc.removeChoice = function (z) {
        //var lastItem = $scope.choiceSet.choices.length - 1;
        bc.choiceSet.choices.splice(z, 1);
    };
    // tags end

    bc.getActiveBlogs = function () { };
    bc.getSelectedBlog = function () {
        var call = "http://localhost:61143/BlogApi/GetBlogByID?id=1";
        var test = $http.get(call).then(function (response) {
            return response;
        }, function (error) {
            alert(error);
            });
        return test;

    };
    bc.addNewBlogPost = function () {
        bc.newBlogPostModel.Tags = bc.choiceSet.choices;
        //bc.taggs = $('#my-tag-list'); $("input").tagsinput('items')
        bc.newBlogPostModel.ContentHtml = bc.quill.container.innerHTML; // or quill.container.firstChild.innerHTML could also work
        bc.newBlogPostModel.ContentText = bc.quill.getText();

        const headers = new Headers();
        var bpToAdd = JSON.stringify(bc.newBlogPostModel);

        var call = "http://localhost:61143/BlogApi/PostBlog";
        var config = {
            headers: {
                'Content-Type': 'application/json'
            }
        };
        $http.post(call, { 'Name': 'bpToAdd', 'Json': JSON.stringify(bc.newBlogPostModel) }).then(function (response) {
            $scope.value = response;
            var newURL = "http://" + $window.location.host + '/Blog/BlogPost?blogID=' + response.data;
            $window.location.href = newURL;
        }, function (error) {
            alert(error.statusText);
        });

    };

    bc.test = function () {
        return false;
    };
    bc.postRequest = function (call) {
        $http.post(call).then(function (response) {
            $scope.value = response;
            $window.location.href = '/Blog/BlogPost/' + response;

        }, function (error) {
            alert(error);
        });
    };

}
function homeCtrl($scope) {
    var bc = this;
    bc.searchText = "";
    bc.Message = "Congratulation you have created your first application using AngularJs";
    bc.tags = ["Politics", "Philosophy", "Bottom Up Systems", "Putin", "Stoicism", "Brexit", "Trump", "History", "Tech"];
}