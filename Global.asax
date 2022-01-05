<%@ Application Language="VB" %>
<%@ Import Namespace="System.Web.Routing" %>

<script runat="server">

    Sub RegisterRoutes(routes As RouteCollection)

        ' routes.MapPageRoute("admin", "admin", "~/admin/admin.aspx")
        routes.MapPageRoute("upload", "upload", "~/upload.aspx")
        routes.MapPageRoute("download", "download", "~/download.aspx")
        routes.MapPageRoute("logout", "logout", "~/logout.aspx")
        routes.MapPageRoute("login", "login", "~/login.aspx")

        'routes.MapPageRoute("admin_forms_action", "admin/forms/{FormAction}/{*FormID}", "~/admin/manage_forms.aspx")
        'routes.MapPageRoute("admin_forms", "admin/forms/", "~/admin/manage_forms.aspx")

        '->                                       admin/form/{formid}/list
        '->                                       admin/form/{formid}/add
        '->                                       admin/form/{formid}/edit/{id}        

        ' routes.MapPageRoute("admin_render_form1", "admin/form/{formid}/{formaction}/{*itemid}", "~/admin/render_form.aspx")


        ' routes.MapPageRoute("DefaultAPI", "api/{API}", "~/api/api.aspx")
        ' routes.MapPageRoute("API_Version_1", "api/v1/{api}", "~/api/api.aspx")
        ' routes.MapPageRoute("API_Version_1_with_action__", "api/v1/{api}/{action}", "~/api/api.aspx")
        ' routes.MapPageRoute("ReportsRoute", "Reports/{Report}", "~/Reports/Default.aspx")
    End Sub

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
        '-> register routes 
        RegisterRoutes(RouteTable.Routes)
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub

</script>