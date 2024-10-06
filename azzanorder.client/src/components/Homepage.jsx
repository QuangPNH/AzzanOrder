import React, { useState } from 'react';
import MenuMainPage from './MenuMainPage';
const Homepage = () => {
    /*
    const genAI = new GoogleGenerativeAI(GEMINI_API_KEY);

    const [search, setSearch] = useState('');
    const [aiResponse, setResponse] = useState('');
    const [loading, setLoading] = useState(false);
    */
    /**
     * Generative AI Call to fetch text insights
     */
    /*
    async function aiRun() {
        setLoading(true);
        setResponse('');
        const model = genAI.getGenerativeModel({ model: "gemini-pro" });
        const prompt = `random meals related to ${search} category with images and prices`;
        const result = await model.generateContent(prompt);
        const response = await result.response;
        const text = response.text();
        setResponse(text);
        setLoading(false);
    }
    */
   /*
    const handleChangeSearch = (e) => {
        setSearch(e.target.value);
    }

    const handleClick = () => {
        aiRun();
    }
    */
    return (
        /*
        <div>
            <div style={{ display: 'flex' }}>
                <input placeholder='Search Food with Category using Generative AI' onChange={(e) => handleChangeSearch(e)} />
                <button style={{ marginLeft: '20px' }} onClick={() => handleClick()}>Search</button>
            </div>

            {
                loading == true && (aiResponse == '') ?
                    <p style={{ margin: '30px 0' }}>Loading ...</p>
                    :
                    <div style={{ margin: '30px 0' }}>
                        <p>{aiResponse}</p>
                    </div>
            }
        </div>
        */
        //<MenuButton title="Menu" imageurl='../src/assets/book.svg' />
        //<ProductCard imageSrc="https://cdn.builder.io/api/v1/image/assets/TEMP/2058a1d549e641fdf84125eca4b1b3f07bb5710599dffea18b6cc6ee0301ecfb?placeholderIfAbsent=true&apiKey=c0efc441fe73418b8b7246db17f848b8" title="Product Title" price="1000" />
        //<ShowMoreLink title="Recently Ordered" url='https://google.com' />
        //<WarningWindow />
        <MenuMainPage />
    );
};

export default Homepage;