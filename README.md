# TECHNOAPP

## Introduction
TechnoApp stands as a cutting-edge e-commerce platform, revolutionizing the way users engage with online shopping. Seamlessly blending innovation with convenience, TechnoApp offers a vast array of products spanning electronics, gadgets, and accessories, all curated to cater to diverse consumer needs.

Within this README file lies a comprehensive guide, meticulously crafted to streamline your journey through TechnoApp's immersive shopping experience. Whether you're a seasoned tech enthusiast or a novice shopper, this guide serves as your compass, navigating you through the myriad features and functionalities that TechnoApp has to offer.

Embark on your exploration of TechnoApp with confidence, as you discover intuitive navigation, personalized recommendations, and seamless checkout processes. With a user-centric approach at its core, TechnoApp ensures that every interaction is not just transactional but an opportunity for delight and satisfaction.

Unlock the potential of TechnoApp to effortlessly discover and acquire the latest gadgets, electronics, and accessories that complement your lifestyle. Join the TechnoApp community today and embark on a journey of discovery, convenience, and unparalleled shopping satisfaction.

## Features

<b>Products Features</b>

| Feature  |  Coded?       | Description  |
|----------|:-------------:|:-------------|
| Add a Product | &#10004; | Ability of Add a Product on the System |
| List Products | &#10004; | Ability of List Products |
| Edit a Product | &#10004; | Ability of Edit a Product |
| Delete a Product | &#10004; | Ability of Delete a Product |

<b>Purchase Features</b>

| Feature  |  Coded?       | Description  |
|----------|:-------------:|:-------------|
| Create a Cart | &#10004; | Ability of Create a new Cart |
| See Cart | &#10004; | Ability to see the Cart and it items |
| Remove a Cart | &#10004; | Ability of Remove a Cart |
| Add Item | &#10004; | Ability of add a new Item on the Cart |
| Remove a Item | &#10004; | Ability of Remove a Item from the Cart |
| Checkout | &#10004; | Ability to Checkout |


https://github.com/ozangulll/TechnoApp/assets/118335871/73988044-8312-4b36-8ca2-5e869ca64699



1. **Entity Framework Core Usage:** Entity Framework Core is an Object-Relational Mapping (ORM) framework that simplifies data access in .NET applications. It is often used in conjunction with the Repository Pattern to provide seamless integration with databases. Entity Framework Core abstracts away the database-specific details and allows developers to work with entities and repositories using LINQ queries.

2. **Product Catalog Management:** The application includes functionality for managing a product catalog. This involves CRUD operations on product entities, such as adding new products, updating existing ones, and deleting obsolete ones.

3. **Membership System (Identity Core):** Utilizes Identity Core for user authentication and authorization. Users can register, log in, and manage accounts, while administrators have access to user management features.

4. **Payment System (Iyzico Payment):** For handling payments, the application integrates with the Iyzico Payment system. This allows users to make secure payments for products or services offered within the application. The payment system provides various payment methods and ensures the security of transactions.
   
## Integrations
The application utilizes APIs such as SendGrid for email delivery and Iyzico for payment processing.

### SendGrid (For Email Delivery):
SendGrid is used by the application to communicate with users and send important notifications. For example, users may receive emails to verify their accounts or receive updates about the status of their orders. The SendGrid API provides a reliable email delivery service, facilitating the communication processes of the application.


https://github.com/ozangulll/TechnoApp/assets/118335871/1133306e-7528-4579-9a03-9f4bc5ea5e29

[Forgot Password with SendGrid]

https://github.com/ozangulll/TechnoApp/assets/118335871/205830ec-0f5c-4590-a849-a541fdf11b46


### Iyzico Payment System:
Iyzico is a payment system used to securely and quickly process payments. Users can make payments for the products they purchase through the application using credit cards or other payment methods, utilizing the Iyzico API. The Iyzico API offers an infrastructure developed for security measures and facilitating payment transactions.


https://github.com/ozangulll/TechnoApp/assets/118335871/ea18b8be-282d-44a2-8e4a-8914fa7c78bb


## Admin User
As an administrator, you hold the reins to TechnoApp's backend operations. To access the administrative dashboard, simply log in with the following credentials:

Username: administrator
Password: TechnoApp0
With these credentials, you gain access to a plethora of administrative tools and functionalities, empowering you to manage users, products, orders, and much more. Take charge of TechnoApp's ecosystem and steer it towards success with confidence and ease.


https://github.com/ozangulll/TechnoApp/assets/118335871/f45b4625-eebf-4f73-b906-d99323d5d4c8



## User(with no privileges)
For users without administrative privileges, TechnoApp still offers a seamless and enriching shopping experience. Simply sign in using your registered credentials to explore a world of electronics, gadgets, and accessories. While you may not have access to administrative features, you still have the power to browse, purchase, and engage with TechnoApp's diverse offerings.



## Pattern
The Repository Pattern is a fundamental design pattern used in software development to separate concerns and improve maintainability, testability, and scalability of applications. It abstracts the data access layer from the rest of the application, providing a centralized way to interact with data.

## Layers

This project primarily consists of the following 4 layers:

1. **Presentation UI Layer:**
   - Contains user interface (UI) components.
   - Handles user interactions.

2. **Application Layer:**
   - Manages business logic and application flow.
   - Processes requests from the presentation layer.

3. **Business Layer:**
   - Communicates with the database.
   - Accesses external services (APIs, file storage services, etc.).

4. **Data Access Layer:**
   - Performs database operations.
   - Reads from and writes to the database.
   - Example technologies: SQL, Object-Relational Mapping (ORM) libraries.

## Conclusion
By leveraging the Repository Pattern within a layered architecture, the application achieves separation of concerns, enhancing maintainability and flexibility for future enhancements and modifications.
## Contributing
If you would like to contribute, you can create a pull request.

## License
This project is licensed under the [MIT License](LICENSE).
