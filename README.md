# webapp-cdn-demo
Azure Web App, CDN - static &amp; dynamic  
Demo: http://tkdxplwebcdn.azurewebsites.net/ 
Important configuration in web.config
   
    <system.webServer> 
      ...
      <!--Rewrite, for cdn/ -> redirect to normal page-->
      <rewrite>
        <rules>
          <rule name="RewriteIncomingCdnRequest" stopProcessing="true">
            <match url="^cdn/(.*)$" />
            <action type="Rewrite" url="{R:1}" />
          </rule>
        </rules>
      </rewrite>
      <!--Static content - 3 h-->
      <staticContent>
        <clientCache cacheControlMode="UseMaxAge" cacheControlMaxAge="3.00:00:00" />
      </staticContent>
    </system.webServer>
    