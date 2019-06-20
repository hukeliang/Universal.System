/* 
 * 页面初始化执行的脚本
 * 首页框架不可少 必须放到Jquery后面
 */

//判读是否手机端 
try {
    var urlhash = window.location.hash;
    console.log(urlhash)
    console.log(urlhash.match("fromapp"))
    console.log(navigator.userAgent)
    console.log(navigator.userAgent.match(/(iPhone|iPod|Android|ios|iPad)/i))
    var urlhash = window.location.hash;
    if (!urlhash.match("fromapp")) {
        if ((navigator.userAgent.match(/(iPhone|iPod|Android|ios|iPad)/i))) {
            window.location = "http://m.com";
        }
    }
}
catch (error) {

}
//远程CDN加速不可用，加载本地库 
if (typeof jQuery === 'undefined' || typeof window.jQuery.fn.emulateTransitionEnd === 'undefined') {
    var scriptArray = new Array()
    scriptArray.push('%3Cscript src="/src/js/jquery-3.3.1.min.js"%3E%3C/script%3E');
    scriptArray.push('%3Cscript src="/src/js/bootstrap.min.js"%3E%3C/script%3E');
    scriptArray.push('%3Cscript src="/src/js/framework-load.js"%3E%3C/script%3E');
    document.write(unescape(scriptArray.join('')));
}


var data = [
    { id: 1, name: "欢迎使用", link: "/src/view/welcome.html", icon: "glyphicon-send", parent_id: 0 },
    { id: 2, name: "系统设置", link: "/src/view/index.html", icon: "glyphicon-wrench", parent_id: 0 },
    { id: 3, name: "菜单管理", link: "#", icon: "glyphicon-th-list", parent_id: 2 },
    { id: 4, name: "图标管理", link: "#", icon: "glyphicon-tag", parent_id: 2 },
    { id: 5, name: "权限管理", link: "#", icon: "glyphicon-lock", parent_id: 2 },
    { id: 6, name: "角色管理", link: "/src/view/account.html", icon: "glyphicon-user", parent_id: 2 },
    { id: 7, name: "用户管理", link: "#", icon: "glyphicon-volume-down", parent_id: 2 },
    { id: 8, name: "无极限菜单", link: "#", icon: "glyphicon-link", parent_id: 0 },
    { id: 9, name: "更多", link: "#", icon: "glyphicon-option-horizontal", parent_id: 0 },
    { id: 10, name: "二级菜单", link: "#", icon: "glyphicon-folder-close", parent_id: 8 },
    { id: 11, name: "三级菜单", link: "#", icon: "glyphicon-eye-open", parent_id: 10 },
    { id: 12, name: "四级菜单", link: "#", icon: "glyphicon-leaf", parent_id: 11 },
    { id: 13, name: "五级菜单", link: "#", icon: "glyphicon-warning-sign", parent_id: 12 },
    { id: 14, name: "六级菜单", link: "#", icon: "glyphicon-bookmark", parent_id: 13 },
    { id: 15, name: "七级菜单", link: "#", icon: "glyphicon-tags", parent_id: 14 },
];
/**
 * 无极限菜单树
 * @param {JSON []} arr 菜单数组信息
 * @param {string} id 菜单主键id
 */
var menuTree = function (arr, id) {
    //处理数据 给菜单等级
    var addLevel = function (arr, id) {
        var data = [], level = 0;
        var addLevel = function (arr, id, level) {
            for (var i in arr) {
                var item = arr[i];
                if (item.parent_id == id) {
                    item.level = level;
                    data.push(item);
                    addLevel(arr, item.id, level + 1);
                }
            }
        };
        addLevel(arr, id, level);
        return data;
    }
    //根据菜单主键id获取下级菜单
    var getChildNodes = function (arr, id) {
        var noders = [];
        for (var i in arr) {
            if (arr[i].parent_id == id) {
                noders.push(arr[i]);
            }
        }
        return noders;
    }
    //判断是否包含子节点
    var isChildNodes = function (arr, id) {
        for (var i in arr) {
            if (arr[i].parent_id == id) {
                return true;
            }
        }
        return false;
    }
    //递归拼接菜单树
    var menuHtml = '', spacing = 20;
    var recursive = function (arr, id) {
        var noders = getChildNodes(arr, id);
        if (noders.length > 0) {
            for (var i in noders) {
                var padding = noders[i].level * spacing + 10; //子节点偏移距离
                menuHtml += '<div class="panel panel-default app-panel">';
                if (isChildNodes(arr, noders[i].id) > 0) {  //判断是否有子菜单
                    menuHtml += '<button type="button" class="btn app-btn app-transition-350" data-toggle="collapse" data-target="#u-muen' + noders[i].id + '" style="padding-left:' + padding + 'px">';
                    menuHtml += '<span class="glyphicon  ' + noders[i].icon + '"></span><abbr class="">' + noders[i].name + '</abbr>';
                    menuHtml += '<span class="glyphicon glyphicon-triangle-right app-muen-icon"></span>';
                    menuHtml += '</button>';
                    menuHtml += '<div id="u-muen' + noders[i].id + '" class="collapse">';
                    recursive(arr, noders[i].id);//递归拼接子菜单
                    menuHtml += '</div>';
                }
                else {
                    menuHtml += '<a id="u-muen-link' + noders[i].id + '" data-stop="default" href="' + noders[i].link + '" class="btn app-btn app-transition-350" style="padding-left:' + padding + 'px">';
                    menuHtml += '<span class="glyphicon ' + noders[i].icon + '"></span><abbr class="">' + noders[i].name + '</abbr>';
                    menuHtml += '</a>';
                }
                menuHtml += '</div>';
            }
        }
    }

    recursive(addLevel(arr, id), id);
    return menuHtml;
}

/**
 * 停止事件
 * @param {event} event
 */
var stopEvent = function (event) {
    window.event ? window.event.cancelBubble = true : event.stopPropagation();
}

/**
 * *获取Guid
 */
var getGuid = function () {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8); return v.toString(16);
    });
}


var clipArray = [];
/**
 * 添加多标签页
 * @param {string} id
 * @param {string} title
 * @param {string} link
 * @param {string} icon
 */
var addClip = function (id, title, href, icon) {
    var $clip = null, clipHtml = '';
    if ($.inArray(id, clipArray) === -1) {

        clipHtml += '<li data-muenId="' + id + '"><a href="' + href + '" data-stop="default"><span class="' + icon + '"></span>' + title + '</a>';
        clipHtml += '<span class="close">&times;</span>';
        clipHtml += '</li>';

        clipArray[clipArray.length] = id;
        $('#app-nav-center .nav-tabs').append(clipHtml);
    }
    $clip = $('[data-muenId="' + id + '"]');
    $clip.siblings().removeClass('app-clip-selected');
    $clip.addClass('app-clip-selected');
}


$(document).ready(function () {
    //添加菜单树
    $('#app-menu-accordion').html(menuTree(data, 0));

    //指定要操作的元素的click事件停止传播—定义属性值data-stop="evnet"的元素点击时停止传播事件
    $('html').on({
        click: function (event) {
            stopEvent(event);
        }
    }, '[data-stop="evnet"]');

    //指定要操作的元素的click事件停止传播—定义属性值data-stop="default"的元素点击时阻止默认行为
    $('html').on({
        click: function (event) {
            event.preventDefault();//阻止默认行为
        }
    }, '[data-stop="default"]');

    //提示工具
    $('html').tooltip({
        container: 'body',
        delay: {
            show: 300,
            hide: 100
        }
    }, '[data-toggle="tooltip"]');

    //给#app-menu-accordion下所有.app-panel的元素绑定bootstrap展开关闭事件
    $('#app-menu-accordion').on({
        'shown.bs.collapse': function (event) {
            stopEvent(event);
            $(this).children('button').children('.app-muen-icon').removeClass('glyphicon-triangle-right').addClass('glyphicon-triangle-bottom');
        },
        'hidden.bs.collapse': function (event) {
            stopEvent(event);
            $(this).children('button').children('.app-muen-icon').removeClass('glyphicon-triangle-bottom').addClass('glyphicon-triangle-right');
        },
    }, '.app-panel');

    //给#app-menu-accordion下所有a标签href不为空的元素绑定click事件
    $('#app-menu-accordion').on({
        click: function () {
            var clipName = $(this).text();
            var clipHref = $(this).attr('href');//把开始的/替换掉.replace(/\//i, '')
            var clipIcon = $(this).children().attr('class');
            var clipId = $(this).attr('id');

            window.location.href = '#link=' + clipHref;
            addClip(clipId, clipName, clipHref, clipIcon);
            $('#app-body').children('iframe').attr('src', clipHref);

        }
    }, 'a[href]')

    //进入/退出 全屏
    $('#app-full-screen').on({
        click: function () {
            var status = $(this).data('status');
            if ('normal' === status) {//进入全屏
                $(this).data('status', 'amplifying');
                $(this).children('span').removeClass('glyphicon-fullscreen').addClass('glyphicon-resize-small');
                var docEle = document.documentElement;
                if (docEle.requestFullscreen) {
                    docEle.requestFullscreen();
                } else if (docEle.mozRequestFullScreen) {
                    docEle.mozRequestFullScreen();
                } else if (docEle.webkitRequestFullScreen) {
                    docEle.webkitRequestFullScreen();
                }
            } else if ('amplifying' === status) {//退出全屏
                $(this).data('status', 'normal');
                $(this).children('span').removeClass('glyphicon-resize-small').addClass('glyphicon-fullscreen');
                var doc = document;
                if (doc.exitFullscreen) {
                    doc.exitFullscreen();
                } else if (doc.mozCancelFullScreen) {
                    doc.mozCancelFullScreen();
                } else if (doc.webkitCancelFullScreen) {
                    doc.webkitCancelFullScreen();
                }
            }
        }
    });

    //计算下拉菜单 高度不够显示滚动条
    var $dropdown = null, dropdownSub = null;
    $('#app-user').on({
        'show.bs.dropdown': function () {
            $dropdown = $(this).children('button').next();
            $dropdown.addClass('fade');
        },
        'shown.bs.dropdown': function () {
            var windowHeight = $(window).outerHeight();

            var dropdownHeight = $dropdown.outerHeight();
            var dropdownWidth = $dropdown.outerWidth();
            var dropdownTop = $dropdown.position().top;

            var result = windowHeight - dropdownTop - dropdownHeight;

            if (result <= 0) {
                $dropdown.outerHeight(windowHeight - dropdownTop - 4)
                $dropdown.outerWidth(dropdownWidth + 15)
            }
            $dropdown.addClass('in');
        },
        'hide.bs.dropdown': function () {
            $dropdown.attr('style', '');
            $dropdown.removeClass('fade in');
            $(dropdownSub).toggle();
        }
    });

    //获取子菜单 显示到外面 防止显示滚动条时候隐藏了二级菜单
    $('#app-user').on({
        click: function () {
            dropdownSub = $(this).data('sub');
            if ($('#app-user').children(dropdownSub).length === 0) {
                $('#app-user').append($(this).next(dropdownSub).prop('outerHTML'));
                $(this).next(dropdownSub).remove();
            }
            else {
                $(dropdownSub).toggle();
            }
            $(dropdownSub).css({ 'margin-right': $dropdown.outerWidth() + 'px' });
        }
    }, '[data-toggle="dropdown-submenu"]');


    //多标签选项卡点击事件
    $('#app-nav-center').on({
        click: function () {
            $(this).siblings().removeClass('app-clip-selected');
            $(this).addClass('app-clip-selected');
        }
    }, 'li');

    //选项卡关闭
    $('#app-nav-center').on({
        click: function () {
            $(this).parent().remove()
        }
    }, '.close');


});