# ToastrNotify
Use [toastr](https://github.com/CodeSeven/toastr/)

## Installation
```
Install-Package CHC.ToastrNotify
```
## Usage
```
    <!--toastr-->
    <link href="~/plugins/toastr.js/toastr.min.css" rel="stylesheet" />
    <!-- jQuery -->
    <script src="~/plugins/jquery/jquery.min.js"></script>
    <!--toastr.js-->
    <script src="~/plugins/toastr.js/toastr.min.js"></script>
    <!--toastr set-->
    <script src="~/plugins/toastr.js/app.js"></script>

    @Html.Notify()
```

```c#
        [HttpGet]
        public ActionResult Index(string returnUrl)
        {
            CHC.ToastrNotify.NotifyHelper.Append(CHC.ToastrNotify.NotifyType.Info, "Message","Title");
            return View();
        }
```

## Options
[examples](https://codeseven.github.io/toastr/demo.html)
