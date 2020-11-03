# Delegated group membership

Is it broken? No. :-) Since this code integrates with Azure Active Directory, it needs some configuration to get going. The build errors are on purpose to draw your attention to those locations.

## Introduction 
This repo contains a sample to show delegated management of group memberships for users in Azure Active Directory. Or more conceptually it shows API operations that are performed on behalf of the user and operations that are performed by the application.

This code is a not a fully working solution to be used in production. It just shows the relevant APIs in my scenario, see below. And the majority of code is reused from the references, see further below. Use a vault to store secrets.

## Scenario
This concept can be used by organizations that have their own Azure Active Directory and want to invite AAD users from 3rd party companies (tenants) and have those companies manage group membership for their users.

It shows a few normal operations on behalf of the signed-in user, such as get the groups I am member of and the applications I can access. And tt shows all the groups that I am indirectly a member of and all users that are a member of those. (This is default functionality in the Azure portal.)

It also shows a special operation to update a user's group membership, on behalf of the app since that goes beyond the permissions of the signed-in user. This is called a [confidential client](https://docs.microsoft.com/en-us/azure/active-directory/develop/authentication-flows-app-scenarios). (This is custom functionality)

## The app
To run the app: ​

Set up an Azure Active Directory (AAD) and two customer AADs with users​: 
* A global admin in the main AAD
* Two regular users in the first customer AAD
* Two regular users in the second customer AAD

For each customer create these AAD groups​:
* A group for all customer1 users
* A group for app1 users of customer1
* A group for app2 users of customer1
* A group for customer1 admins. Make this group a member of the other groups for the same customer.

Create one or two AAD Enterprise Applications and in Users and groups assign authorized groups from above to those apps. 

Go through [these steps](https://github.com/Azure-Samples/active-directory-aspnetcore-webapp-openidconnect-v2/tree/master/2-WebApp-graph-user/2-1-Call-MSGraph) to
* Create an app registration in AAD
* Configure this webapp with the correct values
* Create the enterprise application in AAD with permissions​. For details on the permissions, see the API documentation that shows the required permissions for each operation.

For completeness: Did you configure the app in AAD? And did you also configure the values in the code?

## References
The API documentation shows all operations and the required permissions
https://docs.microsoft.com/en-us/graph/api/overview

This sample (2-1) is the basis of the code in this repo for normal operations
https://github.com/Azure-Samples/active-directory-aspnetcore-webapp-openidconnect-v2

Input for the elevated operations
https://github.com/microsoftgraph/aspnet-snippets-sample
https://docs.microsoft.com/en-us/graph/auth-v2-service
https://github.com/AzureAD/microsoft-identity-web/wiki/daemon-scenarios
