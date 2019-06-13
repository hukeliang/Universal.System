
/* 
 * 页面初始化执行的脚本
 * 首页框架不可少 必须放到Jquery后面
 */

if (typeof jQuery === 'undefined') {
    throw new Error('framework-initialize.js 缺少依赖文件Jquery')
}


$(document).ready(function () {

    var app = {
        // 保存setTimeout返回值处理延时执行
        timeout: null,
        // 媒体查询宽度
        media: 991,
        // 左侧菜单状态 
        menuStatus: null,
    };
    var headerHeight = $('#app-header').height();
    var logoHeight = $("#app-menu-logo").height();

    app.browserChange = function () {
        var windowWidth = $(window).width();
        var windowHeight = $(window).height();

        $('#app-body').height(windowHeight - headerHeight);
        $('#app-menu-scroll').height(windowHeight - logoHeight);
    }

    app.browserChange();

    // 监听游览器大小改变
    $(window).on({
        resize: function () {
            clearTimeout(app.timeout);
            app.timeout = setTimeout(function () {
                app.browserChange();
            }, 200);
        }
    });

});




