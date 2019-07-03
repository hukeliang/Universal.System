/* 
 * 页面初始化执行的脚本
 * 首页框架不可少 必须放到Jquery后面
 */

//判读是否手机端 
try {
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
    { id: 3, name: "菜单管理", link: "/src/view/menu.html", icon: "glyphicon-th-list", parent_id: 2 },
    { id: 4, name: "图标管理", link: "#", icon: "glyphicon-tag", parent_id: 2 },
    { id: 5, name: "权限管理", link: "#", icon: "glyphicon-lock", parent_id: 2 },
    { id: 6, name: "角色管理", link: "/src/view/account.html", icon: "glyphicon-user", parent_id: 2 },
    { id: 7, name: "用户管理", link: "#", icon: "glyphicon-volume-down", parent_id: 2 },
    { id: 8, name: "无极限菜单", link: "#", icon: "glyphicon-link", parent_id: 0 },
    { id: 9, name: "这是一个名字超级长的菜单", link: "/src/view/account.html", icon: "glyphicon-bookmark", parent_id: 0 },
    { id: 10, name: "更多", link: "/src/view/index.html", icon: "glyphicon-option-horizontal", parent_id: 0 },
    { id: 11, name: "二级菜单", link: "#", icon: "glyphicon-folder-close", parent_id: 8 },
    { id: 12, name: "三级菜单", link: "#", icon: "glyphicon-eye-open", parent_id: 11 },
    { id: 13, name: "四级菜单", link: "#", icon: "glyphicon-leaf", parent_id: 12 },
    { id: 14, name: "五级菜单", link: "#", icon: "glyphicon-warning-sign", parent_id: 13 },
    { id: 15, name: "六级菜单", link: "#", icon: "glyphicon-bookmark", parent_id: 14 },
    { id: 16, name: "七级菜单", link: "#", icon: "glyphicon-tags", parent_id: 15 },
    { id: 17, name: "更多测试菜单1", link: "/src/view/menu.html", icon: "glyphicon-tags", parent_id: 0 },
    { id: 18, name: "更多测试菜单2", link: "/src/view/index.html", icon: "glyphicon-tags", parent_id: 0 },
];

/**
 * 停止事件
 * @param {event} event
 */
var stopEvent = function (event) {
    window.event ? window.event.cancelBubble = true : event.stopPropagation();
}



$(document).ready(function () {

    //添加菜单树
    $('#app-menu-accordion').menuTree({ data: data });

    $.frameworkSizeChange();

    //指定要操作的元素的click事件停止传播—定义属性值data-stop="evnet"的元素点击时停止传播事件
    $('body').on({
        click: function (event) {
            $.stopEvent(event);
        }
    }, '[data-stop="evnet"]');

    //指定要操作的元素的click事件停止传播—定义属性值data-stop="default"的元素点击时阻止默认行为
    $('body').on({
        click: function (event) {
            event.preventDefault();//阻止默认行为
        }
    }, '[data-stop="default"]');

    //提示工具
    $('body').tooltip({
        selector: '[data-toggle="tooltip"]',
        container: 'body',
        delay: {
            show: 300,
            hide: 100
        }
    });

    //给#app-menu-accordion下所有.app-panel的元素绑定bootstrap展开关闭事件
    $('#app-menu-accordion').on({
        'shown.bs.collapse': function (event) {
            $.stopEvent(event);
            $(this).children('button').children('.app-muen-icon').removeClass('glyphicon-triangle-right').addClass('glyphicon-triangle-bottom');
        },
        'hidden.bs.collapse': function (event) {
            $.stopEvent(event);
            $(this).children('button').children('.app-muen-icon').removeClass('glyphicon-triangle-bottom').addClass('glyphicon-triangle-right');
        },
    }, '.app-panel');

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

    //多标签页
    $('#app-nav-center').tabstrip({ iframeBodyId: '#app-body' });

});

+function ($) {
    'use strict';
    //从数组中删除指定元素
    $.removeArrayByValue = function (arr, val) {
        for (var i = 0; i < arr.length; i++) {
            if (arr[i] == val) {
                arr.splice(i, 1);
                break;
            }
        }
    }
    //获取Guid
    $.getGuid = function () {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8); return v.toString(16);
        });
    }
    //停止事件
    $.stopEvent = function (event) {
        window.event ? window.event.cancelBubble = true : event.stopPropagation();
    }
}(jQuery);

//多标签页
+function ($) {
    'use strict';
    $.fn.tabstrip = function (options) {
        var tabstripDft = {
            iframeBodyId: '',
            borderMobileTime: 200,
            resizeTime: 100,
            tabstripMobileTime: 100,
        };

        var tabstripOpt = $.extend(tabstripDft, options);

        var $this = $(this);
        var $targetTabstrip = $this.find('[data-tabstrip-select="true"]');
        var $mobileBorder = $this.find('.app-clip-border p');

        var dataSelect = 'data-tabstrip-select';
        var tabstripItem = 'li[id][id!=""]';
        var tabstripChildren = '.nav.nav-tabs';
        var tabstripLeft = '.app-nav-left';
        var tabstripRight = '.app-nav-right';

        //多标签页数组
        var tabstripArr = new Array();
        var tabstripTimeout = null;
        var tabstripTotalWidth = 0; //当前打开的选项卡总宽度
        var tabstripThisWidth = $this.children(tabstripChildren).outerWidth();//默认宽度
        var thisOffsetWidth = $this.offset().left;//当期对象距离document左边的距离
        var tabstripBaseWidth = $this.outerWidth();//父容器总宽度
        var tabstripSwitchLeft = $this.prevAll(tabstripLeft).outerWidth();//左移动默认宽度
        var tabstripSwitchRight = $this.prevAll(tabstripRight).outerWidth();//右移动默认宽度

        //添加多标签页
        var createTabstrip = function (id, title, url, icon) {
            if ($.inArray(id, tabstripArr) === -1) {
                var htmlArr = new Array();
                var htmlIframeArr = new Array();
                htmlArr.push('<li id="' + id.replace('#', '') + '"><a data-stop="default" href="' + url + '"><span class="' + icon + '"></span>' + title + '</a>');
                htmlArr.push('<span class="close">&times;</span>');
                htmlArr.push('</li>');

                htmlIframeArr.push('<iframe frameborder="0" src="' + url + '" data-iframe-source="' + id + '"></iframe>');

                $this.children(tabstripChildren).append(htmlArr.join(''));
                $(tabstripOpt.iframeBodyId).append(htmlIframeArr.join(''));

                tabstripArr[tabstripArr.length] = id;

            }
            $(tabstripOpt.iframeBodyId).find('iframe').hide(0)
            $('[data-iframe-source="' + id + '"]').show(0);
        }

        //移动边框过渡效果
        var mobileBorderTransition = function (thisId, url) {
            if ($targetTabstrip.attr(dataSelect) !== $(thisId).attr(dataSelect)) {
                $targetTabstrip = $(thisId);
                $targetTabstrip.siblings().attr(dataSelect, false);
                $mobileBorder.show(0).animate({ left: $targetTabstrip.position().left }, tabstripOpt.borderMobileTime, function () {
                    $mobileBorder.hide(0);
                    $targetTabstrip.attr(dataSelect, true);
                });
            }
        }

        //计算已经打开的选项卡总宽度
        var calculateTabstrip = function () {
            tabstripTotalWidth = 0;
            $this.find(tabstripItem).each(function () {
                tabstripTotalWidth += $(this).outerWidth();
            });
            $this.children(tabstripChildren).outerWidth(tabstripThisWidth + tabstripTotalWidth);

        }
        //判断是否显示左右切换栏
        var switchTabstrip = function () {
            tabstripBaseWidth = $this.outerWidth();
            if (tabstripTotalWidth >= tabstripBaseWidth) {
                $this.prevAll({ tabstripLeft, tabstripRight }).show(0);
                $this.css({ marginLeft: tabstripSwitchLeft, marginRight: tabstripSwitchRight });
            } else {
                $this.prevAll({ tabstripLeft, tabstripRight }).hide(0);
                $this.css({ marginLeft: 0, marginRight: 0 });
            }
        }

        //当可视距离不够的时候总是显示当前选中的标签页
        var alwaysShow = function () {
            var visualWidth = $this.outerWidth();//重新获取可视宽度
            var currentWidth = $targetTabstrip.position().left + $targetTabstrip.outerWidth();

            if (currentWidth >= visualWidth) {
                console.log('1')
                var reuslt = currentWidth - visualWidth;
                $this.children(tabstripChildren).animate({ left: -reuslt }, tabstripOpt.tabstripMobileTime);
            }  else if (visualWidth >= tabstripTotalWidth) {
                console.log('3')
                $this.children(tabstripChildren).animate({ left: 0 }, tabstripOpt.tabstripMobileTime);
            } else if (thisOffsetWidth + tabstripSwitchLeft > $targetTabstrip.offset().left) {
                console.log('2')
                $this.children(tabstripChildren).animate({ left: -$targetTabstrip.position().left }, tabstripOpt.tabstripMobileTime);
            }
        }
        //多标签选项卡点击事件
        $this.on({
            click: function () {
                createTabstrip('#' + $(this).attr('id'));
                mobileBorderTransition($(this));
                alwaysShow();
            }
        }, tabstripItem);

        //选项卡关闭
        $this.on({
            click: function () {
                //淡出
                $(this).parent().fadeToggle(100, function () {
                    $(this).remove();
                    $.removeArrayByValue(tabstripArr, $(this).data('id'));
                });
            }
        }, 'span.close');

        //来源
        $('[data-tabstrip="#' + $this.attr('id') + '"]').on({
            click: function () {
                var obj = {
                    id: $(this).data('id'),
                    titel: $(this).text(),
                    url: $(this).attr('href'),//把开始的/替换掉.replace(/\//i, '')
                    icon: $(this).children().attr('class')
                }
                window.location.href = '#link=' + obj.url;

                createTabstrip(obj.id, obj.titel, obj.url, obj.icon);
                mobileBorderTransition(obj.id, obj.url);
                calculateTabstrip();
                switchTabstrip();
                alwaysShow();
            }
        });

        //监听游览器大小改变
        $(window).on({
            resize: function () {
                clearTimeout(tabstripTimeout);
                tabstripTimeout = setTimeout(function () {
                    switchTabstrip();
                    alwaysShow();
                }, tabstripOpt.resizeTime);
            }
        });

        //设置默认宽度
        $mobileBorder.width($targetTabstrip.outerWidth());
        $this.find(tabstripItem).each(function () {
            var thisId = '#' + $(this).attr('id');
            if ($(this).attr(dataSelect)) {
                $('[data-iframe-source="' + thisId + '"]').attr('src', $(this).children().attr('href')).show(0);
            }
            tabstripArr[tabstripArr.length] = thisId;
        });

    }
}(jQuery);

//菜单树
+function ($) {
    'use strict';

    $.fn.menuTree = function (options) {

        var menuTreeDft = {
            data: [],
            initId: 0,
            spacing: 20,//子菜单距离左边的距离
        };

        var menuTreeOpt = $.extend(menuTreeDft, options);
        var menuHtml = new Array();
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
        var recursive = function (arr, id) {
            var noders = getChildNodes(arr, id);
            if (noders.length > 0) {
                for (var i in noders) {
                    var padding = noders[i].level * menuTreeOpt.spacing + 10; //子节点偏移距离
                    menuHtml.push('<div class="panel panel-default app-panel">');
                    if (isChildNodes(arr, noders[i].id) > 0) {  //判断是否有子菜单
                        menuHtml.push('<button type="button" class="btn app-btn app-transition" data-toggle="collapse" data-target="#u-muen' + noders[i].id + '" style="padding-left:' + padding + 'px">');
                        menuHtml.push('<span class="glyphicon  ' + noders[i].icon + '"></span><abbr class="">' + noders[i].name + '</abbr>');
                        menuHtml.push('<span class="glyphicon glyphicon-triangle-right app-muen-icon"></span>');
                        menuHtml.push('</button>');
                        menuHtml.push('<div id="u-muen' + noders[i].id + '" class="collapse">');
                        recursive(arr, noders[i].id);//递归拼接子菜单
                        menuHtml.push('</div>');
                    }
                    else {
                        menuHtml.push('<a class="btn app-btn app-transition" data-tabstrip="#app-nav-center" data-stop="default" data-id="#u-muen-link' + noders[i].id + '"  href="' + noders[i].link + '" style="padding-left:' + padding + 'px">');
                        menuHtml.push('<span class="glyphicon ' + noders[i].icon + '"></span><abbr class="">' + noders[i].name + '</abbr>');
                        menuHtml.push('</a>');
                    }
                    menuHtml.push('</div>');
                }
            }
        }

        recursive(addLevel(menuTreeOpt.data, menuTreeOpt.initId), menuTreeOpt.initId);
        $(this).html(menuHtml.join(''));
    }

}(jQuery);

//框架大小变化
+function ($) {
    'use strict';

    $.frameworkSizeChange = function () {

        var frameworkChange = function (headerHeight, logoHeight) {
            var windowHeight = $(window).height();

            $('#app-body').height(windowHeight - headerHeight);
            $('#app-menu-scroll').height(windowHeight - logoHeight);
        }
        //游览器大小改变重新设置头部和logo宽高
        var frameworkChangeTimeout = null,
            frameworkHeaderHeight = $('#app-header').height(),
            frameworkMenuHeight = $("#app-menu-logo").height();

        // 监听游览器大小改变
        $(window).on({
            resize: function () {
                clearTimeout(frameworkChangeTimeout);
                frameworkChangeTimeout = setTimeout(function () {
                    frameworkChange(frameworkHeaderHeight, frameworkMenuHeight);
                }, 200);
            }
        });

        frameworkChange(frameworkHeaderHeight, frameworkMenuHeight);
    }
}(jQuery);