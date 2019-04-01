layui.use(['form', 'layer', 'table', 'laytpl'], function() {
    var form = layui.form,
        layer = parent.layer === undefined ? layui.layer : top.layer,
        $ = layui.jquery,
        laytpl = layui.laytpl,
        table = layui.table;

    var tableIns = table.render({
        elem: '#menuList',
        url: '/Menu/LoadData/',
        cellMinWidth: 95,
        page: true,
        height: 'full-125',
        limits: [10, 15, 20, 25],
        limit: 10,
        id: "menuListTable",
        cols: [
            [
                { type: "checkbox", fixed: "left", width: 50 },
                { field: "Id", title: "Id", minWidth: 30, align: "center" },
                { field: "Name", title: "调用别名", minWidth: 50, align: "center" },
                { field: "DisplayName", title: "显示名称", minWidth: 50, align: "center" },
                { field: "LinkUrl", title: "链接地址", minWidth: 80, align: "center" },
                { field: "Sort", title: "排序数字", minWidth: 80, align: "center" },
                {
                    field: "IsDisplay",
                    title: "是否显示",
                    minWidth: 100,
                    fixed: "right",
                    align: "center",
                    templet: "#IsDisplay"
                },
                { field: "操作", minWidth: 80, templet: "#menuListBar", fixed: "right", align: "center" }
            ]
        ]
    });
})