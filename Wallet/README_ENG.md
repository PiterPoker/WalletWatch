### **Wallet Microservice**

## **Description**

The Wallet API is a microservice developed using .NET 8.0 that provides functionalities for managing family wallets and personal accounts. This microservice is part of a larger system and interacts with other microservices.

## **Architecture**

### **Core Components:**
* **Data Models:** Define the data structure for wallets and personal accounts.
* **Services:** Manage the application's business logic.
* **Controllers:** Handle HTTP requests and interact with services.

## **REST API Requests**

### **Family Wallets**

1. **Create a new wallet**
   - **URL**: `/api/wallets`
   - **Method**: POST
   - **Description**: Creates a new family wallet with specified parameters.

2. **Get all wallets with pagination**
   - **URL**: `/api/wallets`
   - **Method**: GET
   - **Description**: Returns a list of all wallets with pagination.

3. **Get a wallet by ID**
   - **URL**: `/api/wallets/{id}`
   - **Method**: GET
   - **Description**: Returns information about a wallet with the specified ID.

4. **Update wallet information**
   - **URL**: `/api/wallets/{id}`
   - **Method**: PUT
   - **Description**: Updates information about a wallet with the specified ID.

5. **Delete a wallet**
   - **URL**: `/api/wallets/{id}`
   - **Method**: DELETE
   - **Description**: Deletes a wallet with the specified ID.

6. **Transfer funds between wallets**
   - **URL**: `/api/wallets/TransferFunds`
   - **Method**: POST
   - **Description**: Transfers the specified amount of money between the main wallet and a subwallet.

### **Subwallets**

1. **Create a new subwallet**
   - **URL**: `/api/subwallets`
   - **Method**: POST
   - **Description**: Creates a new subwallet with specified parameters.

2. **Get all subwallets with pagination**
   - **URL**: `/api/subwallets`
   - **Method**: GET
   - **Description**: Returns a list of all subwallets with pagination.

3. **Get a subwallet by ID**
   - **URL**: `/api/subwallets/{id}`
   - **Method**: GET
   - **Description**: Returns information about a subwallet with the specified ID.

4. **Update subwallet information**
   - **URL**: `/api/subwallets/{id}`
   - **Method**: PUT
   - **Description**: Updates information about a subwallet with the specified ID.

5. **Delete a subwallet**
   - **URL**: `/api/subwallets/{id}`
   - **Method**: DELETE
   - **Description**: Deletes a subwallet with the specified ID.

6. **Transfer funds between subwallets**
   - **URL**: `/api/subwallets/TransferFunds`
   - **Method**: POST
   - **Description**: Transfers the specified amount of money between subwallets.

### **Personal Accounts**

1. **Create a new personal account**
   - **URL**: `/api/accounts`
   - **Method**: POST
   - **Description**: Creates a new personal account with specified parameters.

2. **Get all personal accounts with pagination**
   - **URL**: `/api/accounts`
   - **Method**: GET
   - **Description**: Returns a list of all personal accounts with pagination.

3. **Get a personal account by ID**
   - **URL**: `/api/accounts/{id}`
   - **Method**: GET
   - **Description**: Returns information about a personal account with the specified ID.

4. **Update personal account information**
   - **URL**: `/api/accounts/{id}`
   - **Method**: PUT
   - **Description**: Updates information about a personal account with the specified ID.

5. **Delete a personal account**
   - **URL**: `/api/accounts/{id}`
   - **Method**: DELETE
   - **Description**: Deletes a personal account with the specified ID.

6. **Transfer funds between personal accounts**
   - **URL**: `/api/accounts/TransferFunds`
   - **Method**: POST
   - **Description**: Transfers the specified amount of money between personal accounts.

7. **Add funds to a personal account**
   - **URL**: `/api/accounts/AddFunds`
   - **Method**: POST
   - **Description**: Adds the specified amount of money to a personal account.

## **Docker**

The project is deployed using Docker and uses a Dockerfile to create the application image.
