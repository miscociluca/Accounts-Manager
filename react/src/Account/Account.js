import logo from '../logo.svg';
import '../App.css';
import {Link, useNavigate, useParams} from 'react-router-dom';
import {Fragment, useState} from "react";
import {Button, FormControl, FormGroup, Grid, Input, InputLabel} from "@mui/material";
import Title from "../Shared/Title";

const Account = () => {
    const {id} = useParams();
    const navigate = useNavigate();
    const [createAccountRequest, setAccountRequest] = useState({
        customerId: id,
        initialCredit: 0
    })

    const handleSubmit = async (event) => {
        event.preventDefault();

        await fetch('https://localhost:44309/api/Account', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(createAccountRequest),
        });
        navigate('/');
    }

    const handleChange = (event) => {
        const target = event.target;
        const value = target.value;
        const name = target.name;
        let newAccountRequest = {...createAccountRequest};
        newAccountRequest[name] = value;
        setAccountRequest(newAccountRequest);
    }

    return (
        <div className="App">
            <header className="App-header">
                <img src={logo} className="App-logo" alt="logo"/>
                <div className="App-intro">
                    <Fragment>
                        <Title>Create Account</Title>
                        <form onSubmit={handleSubmit}>
                            <Grid container spacing={3} sx={{mt: 3}}>
                                <Grid item xs={12} md={6} lg={6}>
                                    <FormControl>
                                        <InputLabel htmlFor="customer-id">Customer Id</InputLabel>
                                        <Input id="customer-id"
                                               value={createAccountRequest.customerId || ''} disabled/>
                                    </FormControl>
                                </Grid>
                                <Grid item xs={12} md={6} lg={6}>
                                    <FormControl>
                                        <InputLabel htmlFor="initialCredit">Initial Credit</InputLabel>
                                        <Input name="initialCredit" id="initialCredit"
                                               value={createAccountRequest.initialCredit}
                                               onChange={(e) => handleChange(e)} autoComplete="initialCredit"/>
                                    </FormControl>
                                </Grid>
                                <Grid container sx={{mt: 3}}>
                                    <Grid item xs={12} md={6} lg={6}>
                                        <Button variant="contained" color="primary" type="submit">Save</Button>
                                    </Grid>
                                    <br/>
                                    <Grid item xs={12} md={6} lg={6}>
                                        <Button variant="contained" color="secondary" onClick={() => {navigate('/')}} to="/">Cancel</Button>
                                    </Grid>
                                </Grid>
                            </Grid>
                        </form>
                    </Fragment>
                </div>

            </header>
        </div>
    );
}
export default Account;
