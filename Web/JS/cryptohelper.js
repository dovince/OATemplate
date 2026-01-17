// 引入 CryptoJS 4.1.1 库
// <script src="https://cdnjs.cloudflare.com/ajax/libs/crypto-js/4.1.1/crypto-js.min.js"></script>

// AES 加密类
function AesEncryption(options) {
    // 默认配置
    var defaults = {
        key: "EY8WePvjM5GGwQzn",
        iv: null,
        useHex: false
    };

    // 合并配置
    var config = $.extend({}, defaults, options);

    // 初始化密钥和 IV
    this.key = config.key ? CryptoJS.enc.Utf8.parse(config.key) : null;
    this.iv = config.iv ? CryptoJS.enc.Utf8.parse(config.iv) : null;
    this.useHex = !!config.useHex;
}

// 获取加密选项
AesEncryption.prototype.getOptions = function () {
    return {
        mode: CryptoJS.mode.ECB,
        padding: CryptoJS.pad.Pkcs7,
        iv: this.iv
    };
};

// AES 加密方法
AesEncryption.prototype.encryptByAES = function (cipherText) {
    // 执行加密
    var encrypted = CryptoJS.AES.encrypt(cipherText, this.key, this.getOptions());
    var result = encrypted.toString();

    // 格式转换（如果需要转为 HEX 格式）
    return this.useHex
        ? CryptoJS.enc.Hex.stringify(CryptoJS.enc.Base64.parse(result))
        : result;
};

// AES 解密方法
AesEncryption.prototype.decryptByAES = function (cipherText) {
    // 格式转换（如果是 HEX 格式，先转回 Base64）
    var realCipherText = this.useHex
        ? CryptoJS.enc.Base64.stringify(CryptoJS.enc.Hex.parse(cipherText))
        : cipherText;

    // 执行解密
    var bytes = CryptoJS.AES.decrypt(realCipherText, this.key, this.getOptions());
    return bytes.toString(CryptoJS.enc.Utf8);
};

// Base64 编码/解码工具函数
function encryptByBase64(cipherText) {
    return CryptoJS.enc.Utf8.parse(cipherText).toString(CryptoJS.enc.Base64);
}

function decodeByBase64(cipherText) {
    return CryptoJS.enc.Base64.parse(cipherText).toString(CryptoJS.enc.Utf8);
}

// MD5 加密工具函数
function encryptByMd5(password) {
    return CryptoJS.MD5(password).toString();
}