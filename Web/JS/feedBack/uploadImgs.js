var uploadImg = function () {

    var input = document.getElementById("file_input");
    var result;
    var dataArr = []; // 储存所选图片的结果(文件名和base64数据)  
    var fd;  //FormData方式发送请求    
    var oSelect = document.getElementById("select");
    var oAdd = document.getElementById("add");
    var oSubmit = document.getElementById("submit");
    var oInput = document.getElementById("file_input");

    function readFile() {
        fd = new FormData();
        var iLen = input.files.length;
        blockUI({ target: "page-container", message: "处理中..." });
        for (var i = 0; i < iLen; i++) {
            if (!input['value'].match(/.jpg|.gif|.png|.jpeg|.bmp/i)) {　　//判断上传文件格式    
                return alert("上传的图片格式不正确，请重新选择");
            }
            var reader = new FileReader();
            fd.append(i, input.files[i]);
            reader.readAsDataURL(input.files[i]);  //转成base64    
            reader.fileName = input.files[i].name;

            reader.onload = function (e) {
                var imgMsg = {
                    name: this.fileName,//获取文件名    
                    base64: this.result   //reader.readAsDataURL方法执行完后，base64数据储存在reader.result里    
                }
                dataArr.push(imgMsg);
                result = '<div class="result"><div class="delete"></div><img class="subPic" src="' + this.result + '" alt="' + this.fileName + '"/></div>';
                var div = document.createElement('div');
                div.innerHTML = result;
                div['className'] = 'float';
                document.getElementById('imgs').appendChild(div);  　　//插入dom树    
                var img = div.getElementsByTagName('img')[0];
                img.onload = function () {
                    var nowHeight = ReSizePic(this); //设置图片大小    
                    this.parentNode.style.display = 'block';
                    var oParent = this.parentNode;
                    if (nowHeight) {
                        oParent.style.paddingTop = (oParent.offsetHeight - nowHeight) / 2 + 'px';
                    }
                }

                $("#HiddenField_FileNames")[0].value += this.fileName+"|";

                //放大图片
                div.children[0].children[1].onclick = function () {
                    var win = window.open();
                    var data = div.children[0].children[1].src;
                    win.document.write('<iframe src="' + data + '" frameborder="0" style="border:0; width:100%; height:100%;" scrolling="no" allowfullscreen></iframe>');
                }

                //在页面中删除该图片元素
                div.children[0].firstElementChild.onclick = function (div) {
                    $("#HiddenField_FileNames")[0].value = $("#HiddenField_FileNames")[0].value.replace($(this).next()[0].alt + "|", "");
                    $(this).parent().parent().remove();
                   
                }
            }
        }
        $.unblockUI();
    }

    function send() {
        var submitArr = [];
        $('.subPic').each(function () {
            submitArr.push({
                name: $(this).attr('alt'),
                base64: $(this).attr('src')
            });
        }
        );
        $.ajax({
            url: 'http://123.206.89.242:9999',
            type: 'post',
            data: JSON.stringify(submitArr),
            dataType: 'json',
            //processData: false,   用FormData传fd时需有这两项    
            //contentType: false,    
            success: function (data) {
                console.log('返回的数据：' + JSON.stringify(data))
            }
        })
    }

    function ReSizePic(ThisPic) {
        var RePicWidth = 200; //这里修改为您想显示的宽度值    

        var TrueWidth = ThisPic.width; //图片实际宽度    
        var TrueHeight = ThisPic.height; //图片实际高度    

        if (TrueWidth > TrueHeight) {
            //宽大于高    
            var reWidth = RePicWidth;
            ThisPic.width = reWidth;
            //垂直居中    
            var nowHeight = TrueHeight * (reWidth / TrueWidth);
            return nowHeight;  //将图片修改后的高度返回，供垂直居中用    
        } else {
            //宽小于高    
            var reHeight = RePicWidth;
            ThisPic.height = reHeight;
        }
    }

    return {
        init: function () {
            if (typeof FileReader === 'undefined') {
                //alert("抱歉，你的浏览器不支持 FileReader");
                input.setAttribute('disabled', 'disabled');
            }
            else {
                input.addEventListener('change', readFile, false);
            }

            //如果是修改页面
            if (getUrlParms("ID") != "") {
                $(".subPic").each(function () {
                    $(this)[0].onclick = function () {
                        window.open(this.src);
                    }
                });

                $(".delete").each(function () {
                    $(this)[0].onclick = function () {
                        $("#HiddenField_FileNames")[0].value = $("#HiddenField_FileNames")[0].value.replace($(this).next()[0].alt + "|", "");
                        $(this).parent().parent().remove();
                    }
                });
            }

            //oSelect.onclick = function () {
            //    oInput.value = "";   // 先将oInput值清空，否则选择图片与上次相同时change事件不会触发  
            //    //清空已选图片  
            //    $('.float').remove();
            //    oInput.click();
            //}

            oAdd.onclick = function () {
                oInput.value = "";   // 先将oInput值清空，否则选择图片与上次相同时change事件不会触发  
                oInput.click();
            }

            //oSubmit.onclick = function () {
            //    if (!dataArr.length) {
            //        return alert('请先选择文件');
            //    }
            //    send();
            //}
        }
    }
}();




