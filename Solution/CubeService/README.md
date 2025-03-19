# API Testing Guide

This document provides example requests to test different API methods. All requests have been verified using Postman.

## Endpoints and Expected Responses

### 1. Reset Cube

- **Method:** `POST`  
    - **URL:** [`http://localhost:5295/api/Cube/Reset`](http://localhost:5295/api/Cube/Reset)  
    - **Expected Response:** `200 OK`  

### 2. Shuffle Cube

- **Method:** `POST`  
    - **URL:** [`http://localhost:5295/api/Cube/ApplyShuffle`](http://localhost:5295/api/Cube/ApplyShuffle)  
    - **Expected Response:** `200 OK`  

### 3. Perform Move

- **Method:** `POST`  
  - **URL:** [`http://localhost:5295/api/Cube/PerformMove?move=D`](http://localhost:5295/api/Cube/PerformMove?move=D)  
  - **Expected Response:** `501 Not Implemented` (Exception thrown)  

- **Method:** `POST`  
  - **URL:** [`http://localhost:5295/api/Cube/PerformMove`](http://localhost:5295/api/Cube/PerformMove)  
  - **Expected Response:** `400 BAD REQUEST`  

- **Method:** `POST`  
  - **URL:** [`http://localhost:5295/api/Cube/PerformMove?move=NotAMove`](http://localhost:5295/api/Cube/PerformMove?move=NotAMove)  
  - **Expected Response:** `400 BAD REQUEST`  

### 4. Get Cube State

- **Method:** `GET`  
  - **URLs:**  
    - [`http://localhost:5295/api/Cube/State`](http://localhost:5295/api/Cube/State)  
    - [`http://localhost:5295/api/Cube`](http://localhost:5295/api/Cube)  
  - **Expected Response:**
    ```json
    {
        "Back": [5,5,5,5,5,5,5,5,5],
        "Bottom": [0,0,0,0,0,0,0,0,0],
        "Front": [2,2,2,2,2,2,2,2,2],
        "Left": [3,3,3,3,3,3,3,3,3],
        "Right": [4,4,4,4,4,4,4,4,4],
        "Top": [1,1,1,1,1,1,1,1,1]
    }

### 5. Get Face State

- **Method:** `GET`  
  - **URL:** [`http://localhost:5295/api/Cube/Face`](http://localhost:5295/api/Cube/Face)  
  - **Expected Response:** `400 BAD REQUEST`  

- **Method:** `GET`  
  - **URL:** [`http://localhost:5295/api/Cube/Face?face=Toop`](http://localhost:5295/api/Cube/Face?face=Toop)  
  - **Expected Response:** `400 BAD REQUEST`  

- **Method:** `GET`  
  - **URLs:**  
    - [`http://localhost:5295/api/Cube/Face?face=Top`](http://localhost:5295/api/Cube/Face?face=Top)  
    - [`http://localhost:5295/api/Cube/Face?face=tOp`](http://localhost:5295/api/Cube/Face?face=tOp)  
    - [`http://localhost:5295/api/Cube/Face?face=tOp`](http://localhost:5295/api/Cube/Face?face=toP)  
  - **Expected Response:**
    ```json
    [1,1,1,1,1,1,1,1,1]
    ```