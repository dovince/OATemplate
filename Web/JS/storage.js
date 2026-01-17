
(function ($) {
    // 创建Storage对象
    function Storage(namespace) {
        this.namespace = namespace || '';
        this.data = {};
        this.load();
    }

    // 从localStorage加载数据
    Storage.prototype.load = function () {
        var stored = localStorage.getItem(this.namespace);
        if (stored) {
            try {
                this.data = JSON.parse(stored);
            } catch (e) {
                this.data = {};
            }
        }
    };

    // 保存数据到localStorage
    Storage.prototype.save = function () {
        localStorage.setItem(this.namespace, JSON.stringify(this.data));
    };

    // 设置值
    Storage.prototype.set = function (key, value) {
        this.data[key] = value;
        this.save();
    };

    // 获取值
    Storage.prototype.get = function (key) {
        return this.data[key];
    };

    // 删除值
    Storage.prototype.remove = function (key) {
        delete this.data[key];
        this.save();
    };

    // 清空
    Storage.prototype.clear = function () {
        this.data = {};
        this.save();
    };

    // 代理方法，实现state.xxx方式访问
    Storage.prototype.proxy = function () {
        var self = this;
        return new Proxy({}, {
            get: function (target, prop) {
                return self.get(prop);
            },
            set: function (target, prop, value) {
                self.set(prop, value);
                return true;
            }
        });
    };

    // 注册到jQuery
    $.storage = function (namespace) {
        return new Storage(namespace).proxy();
    };
})(jQuery);
