import { fabric } from './index'

export class ActionAlign {
    constructor(canvas) {
        // 绘制 3个 rect
        let rect = new fabric.Rect(Object.assign(Rect.defaultRect(), { top: 50, left: 50, fill: 'red' }));
        let rect2 = new fabric.Rect(Object.assign(Rect.defaultRect(), { top: 150, left: 150, fill: 'yellow' }));
        let rect3 = new fabric.Rect(Object.assign(Rect.defaultRect(), { top: 250, left: 250, fill: 'orange' }));
        // 添加图形至画布
        canvas.add(rect);
        canvas.add(rect2);
        canvas.add(rect3);
        // 对齐
        // 绑定点击快捷方式
        this.bindMouseClick(canvas);
    }

    /**
     * 将执行方法绑定到鼠标右键，以供快速测试功能可用性
     */
    bindMouseClick(canvas) {
        $('#labelcanvas').on('mousedown', (e) => {
            let type = e.originalEvent.which;
            switch (type) {
                case 1:
                    // console.log('left');
                    break;
                case 2:
                    // console.log('wheel');
                    // left right top bottom horizontalCenter verticalCenter horizontalEqual verticalEqual
                    this.doAlign(canvas, 'horizontalCenter');
                    break;
                case 3:
                    // console.log('right');
                    break;
            }
        });
    }

    /**
     * 对齐方法实现
     */
    doAlign(canvas, pos) {
        console(1)
        if (canvas._activeObject.length < 2) {
            return false;
        }
        // 记录总宽或总高
        let totalL = 0, arrL = [], eachL = 0;
        if (['horizontalEqual', 'verticalEqual'].includes(pos)) {
            let horizontal = 'horizontalEqual' === pos;
            // 水平/垂直等分 需要排序
            canvas._activeObject._objects.sort((a, b) => {
                return horizontal ? a.aCoords.tl.x - b.aCoords.tl.x : a.aCoords.tl.y - b.aCoords.tl.y;
            });
            // 记录每个元素 宽度/高度，并记录总宽/高
            canvas._activeObject._objects.forEach((item) => {
                arrL.push(horizontal ? item.width : item.height);
                totalL += horizontal ? item.width : item.height;
            });
            // 根据 total 计算等分间隔
            eachL = ((horizontal ? canvas._activeObject.width : canvas._activeObject.height) - 1 - totalL) / (canvas._activeObject._objects.length - 1);
        }
        canvas._activeObject._objects.forEach((item, sub) => {
            item.set(this.getPositions(canvas, pos, item, sub, eachL, arrL));
        });
        // 渲染
        canvas.renderAll();
    }

    /**
     * 根据传入 canvas ，激活对象 item，对齐标志，计算返回相应属性值
     * canvas._activeObject 选中多个元素，最外层的元素
     * canvas._activeObject._objects 选中的所有元素
     * @param canvas Object 画布对象
     * @param pos string 动作
     * @param item Object 每一个选中对象
     * @param sub number item 对应数组下标，水平/垂直等分会用到
     * @param eachL number 等间距的宽/高
     * @param arrL Array 元素 宽/高 数组
     */
    getPositions(canvas, pos, item, sub, eachL, arrL) {
        let params = {};
        let cHeight = canvas._activeObject.height;
        let csHeight = canvas._activeObject.height - 1; // 去除误差，可能为选项框宽度
        let cWidth = canvas._activeObject.width;
        let csWidth = canvas._activeObject.width - 1; // 去除误差，可能为选项框宽度
        switch (pos) {
            case 'top':
                params = {
                    top: 0 - cHeight * 0.5,
                };
                break;
            case 'left':
                params = {
                    left: 0 - cWidth * 0.5,
                };
                break;
            case 'right':
                params = {
                    left: csWidth - item.width - cWidth * 0.5,
                };
                break;
            case 'bottom':
                params = {
                    top: csHeight - item.height - cHeight * 0.5,
                };
                break;
            case 'horizontalCenter': // 水平居中
                params = {
                    top: (csHeight - item.height) * 0.5 - cHeight * 0.5,
                };
                break;
            case 'verticalCenter': // 垂直居中
                params = {
                    left: (csWidth - item.width) * 0.5 - cWidth * 0.5,
                };
                break;
            case 'horizontalEqual': // 水平等分
            case 'verticalEqual': // 垂直等分
                // 首项末项不需要动
                if (sub !== 0 && sub !== canvas._activeObject._objects.length - 1) {
                    // 计算当前元素之前元素的宽度并加上相应间距*sub
                    let total = 0, isHorizontalEqual = 'horizontalEqual' === pos;
                    arrL.map((l, s) => {
                        if (s > sub) {
                            total += l;
                        }
                    });
                    params[isHorizontalEqual ? 'left' : 'top'] = total + sub * eachL - (isHorizontalEqual ? csWidth : csHeight) * 0.5;
                }
                break;
            default:
                break;
        }
        return params;
    }

}