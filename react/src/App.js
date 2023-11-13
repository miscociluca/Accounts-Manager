import './App.css';
import {BrowserRouter as Router, Route, Routes} from 'react-router-dom';
import Customer from "./Customer/Customer";
import Account from "./Account/Account";

function App() {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<Customer/>}/>
                <Route exact  path="/account/:id"  element={<Account/>}/>
            </Routes>
        </Router>
    )
}



export default App;
