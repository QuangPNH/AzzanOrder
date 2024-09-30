export const LOCALHOST_API = "http://localhost:7183"

//export const LOCALHOST_API = "https://swp391-1817-net-g2.techtheworld.id.vn"


// API ACCOUNT
export const GET_ALL_ACCOUNTS = `${LOCALHOST_API}/api/Account/all`
export const GET_ACCOUNT_BY_ID = `${LOCALHOST_API}/api/Account`;

// API CATEGORY

// API MESSENGERBOX
// API EMPLOYEE <=> account
export const LIST_Employee = `${LOCALHOST_API}/employee`;
export const UPDATE_Employee_ID = `${LOCALHOST_API}/api/Account`
export const CREATE_ACCOUNT_EMPLOYEE = `${LOCALHOST_API}/api/Account`;
// API POST
export const UPDATE_ACCOUNT_ID = `${LOCALHOST_API}/api/Account/UPDATE/V2`

// API CATEGORY

// API ORDERS
export const LIST_ORDER = `${LOCALHOST_API}/api/Order/v1/orders/store`
export const LIST_ORDERHaveTableName = `${LOCALHOST_API}/listHaveNameByStoreId`

export const LIST_ORDERDETAILS = `${LOCALHOST_API}/api/Order/OrderDetail`

// API MESSAGE
export const GET_MESSAGE = `${LOCALHOST_API}/api/messages`;
export const DELETE_MESSAGE = `${LOCALHOST_API}/api/messages/delete`
export const DELETE_MESSAGE_STORE = `${LOCALHOST_API}/api/messages/deletes`
// CHAT
export const CHAT_API = `${LOCALHOST_API}/Chat`;


// API STORE
export const GET_STORES_STATUS = `${LOCALHOST_API}/api/status`;
export const STORES_DTOS = `${LOCALHOST_API}/api/storesDtos`;
export const LIST_STORES = `${LOCALHOST_API}/api/stores`;
export const CREATE_STORE = `${LOCALHOST_API}/api/stores/POST`;
export const DELETE_STORE_ID = `${LOCALHOST_API}/api/stores/delete/`;
export const STORE_DETAIL = `${LOCALHOST_API}/api/stores/`;
export const UPDATE_STORE = `${LOCALHOST_API}/api/stores/Update`;
export const SEARCH_STORE = `${LOCALHOST_API}/api/stores/search`;
export const NEW_STORE = `${LOCALHOST_API}/api/stores/newStore`;
// API Category
export const API_CATEGORY = `${LOCALHOST_API}/api/Category`
export const CREATE_CATEGORY = `${LOCALHOST_API}/api/Category/add_new`;
// API ACCOUNTDTOS
export const GET_ACCOUNT_BY_AUTH = `${LOCALHOST_API}/api/AccountDtos/GET`
export const GET_ACCOUNT_BY_TOKEN = `${LOCALHOST_API}/api/AccountDtos/GET`
// API TABLE
export const TABLE = `${LOCALHOST_API}/api/tables`;
export const LIST_ACCOUNT = `${LOCALHOST_API}/api/Account`;
export const LIST_TABLE = `${LOCALHOST_API}/api/tables/store`;

// API PRODUCT CONTROLLERS


export const LIST_PRODUCT_DTOS = `${LOCALHOST_API}/api/productDtos`;
//API ACCOUNT MANAGERS


export const LIST_ACCOUNT_MANAGERS = `${LOCALHOST_API}/api/Account/manager`;
export const CREATE_ACCOUNT_MANAGER = `${LOCALHOST_API}/api/Account`;
export const UPDATE_ACCOUNT_MANAGER = `${LOCALHOST_API}api/Account/`;
// API PRODUCTDTOS

export const GET_PRODUCTSIZES_BY_ID = `${LOCALHOST_API}/api/ProductSizes/id?id=`;
export const CREATE_PRODUCT = `${LOCALHOST_API}/api/ProductSizes/Create`;
export const UPDATE_PRODUCT = `${LOCALHOST_API}/api/ProductSizes/Updateproduct`;
// API PRODUCTSIZES
export const LIST_FOUR_PRODUCT_SIZE_MIN = `${LOCALHOST_API}/api/ProductSizes/getFourProductMin`
export const LIST_FOUR_PRODUCT_SIZE_MAX = `${LOCALHOST_API}/api/ProductSizes/getFourProductMax`

export const LIST_PRODUCT_SIZE = `${LOCALHOST_API}/api/ProductSizes`;
// API TABLE
export const CREATE_TABLE = `${LOCALHOST_API}/api/tables/AddTable`;

// API FeedBack <=> messegerbox
export const LIST_FEEDBACK = `${LOCALHOST_API}/api/MessengerBox`
// API EMPLOYEE <=> account




// LINK SHOP CLIENT
export const LIST_PRODUCT = `http://localhost:3000/listProduct/`;
export const API_ORDER = `${LOCALHOST_API}/api/Order`;

// LINK BLOG
export const GET_BLOGS_STATUS = `${LOCALHOST_API}/api/status`;
export const LIST_BLOGS = `${LOCALHOST_API}/api/Post`;
export const CREATE_BLOG = `${LOCALHOST_API}/api/Post/add_new`;
export const BLOG_DETAIL = `${LOCALHOST_API}/api/Post`;
export const DELETE_BLOG_ID = `${LOCALHOST_API}/api/Post/delete_post/`;
export const UP_BLOG_ID = `${LOCALHOST_API}/api/Post/up_post/`;
export const UPDATE_BLOG = `${LOCALHOST_API}/api/Post/update_post`;

// OrderHub
export const connectOrderHub = `${LOCALHOST_API}/OrderHub`;
