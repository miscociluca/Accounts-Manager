import logo from '../logo.svg';
import '../App.css';
import {Component, Fragment} from "react";
import Title from '../Shared/Title';
import {
    Avatar,
    Link, ListItem, ListItemAvatar,
    ListItemText,
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableRow,

} from "@mui/material";
import AccountIcon from '@mui/icons-material/Money';
import moment from 'moment';

class Customer extends Component {
    state = {
        customers: [{
            accounts: [{
                transactions: []
            }]
        }],
        openTransactionsModal: false
    };

    async componentDidMount() {
        const response = await fetch('https://localhost:44309/api/Customer');
        const body = await response.json();
        this.setState({customers: body});
    }

    handleOpen = () => this.setState({openTransactionsModal: true});
    handleClose = () => this.setState({openTransactionsModal: false});

    render() {
        const {customers} = this.state;
        return (
            <div className="App">
                <header className="App-header">
                    <img src={logo} className="App-logo" alt="logo"/>
                    <div className="App-intro">
                        <Fragment>
                            <Title>Customers</Title>
                            <Table size="medium">
                                <TableHead>
                                    <TableRow>
                                        <TableCell>Name</TableCell>
                                        <TableCell>SurName</TableCell>
                                        <TableCell>Accounts</TableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {customers.map((row) => (
                                        <TableRow key={row.id}>
                                            <TableCell>{row.name}</TableCell>
                                            <TableCell>{row.surName}</TableCell>
                                            <TableCell>
                                                {row.accounts.length > 0 &&
                                                    <Table size="small">
                                                        <TableHead>
                                                            <TableRow>
                                                                <TableCell>Account</TableCell>
                                                                <TableCell>Balance</TableCell>
                                                                <TableCell>Creation Date</TableCell>
                                                                <TableCell>Transactions</TableCell>
                                                            </TableRow>
                                                        </TableHead>
                                                        <TableBody>
                                                            {row.accounts.map(account =>
                                                                (
                                                                    <TableRow key={account.id}>
                                                                        <TableCell>{account.id}</TableCell>
                                                                        <TableCell>{account.balance}</TableCell>
                                                                        <TableCell>{account.creationDate && moment(account.creationDate).format('MMMM Do YYYY, h:mm:ss a')}</TableCell>
                                                                        <TableCell>
                                                                            {account.transactions &&
                                                                                account.transactions.map(transaction =>
                                                                                    (
                                                                                        <ListItem>
                                                                                            <ListItemAvatar>
                                                                                                <Avatar>
                                                                                                    <AccountIcon/>
                                                                                                </Avatar>
                                                                                            </ListItemAvatar>
                                                                                            <ListItemText
                                                                                                primary={transaction.id}
                                                                                                secondary={moment(transaction.transactionDate).format('MMMM Do YYYY, h:mm:ss a')}/>
                                                                                        </ListItem>
                                                                                    )
                                                                                )
                                                                            }
                                                                        </TableCell>
                                                                    </TableRow>
                                                                )
                                                            )}
                                                        </TableBody>
                                                    </Table>
                                                }
                                            </TableCell>
                                            <TableCell>
                                                <Link color="primary" href={`/account/${row.id}`} sx={{mt: 3}}>
                                                    New account
                                                </Link>
                                            </TableCell>
                                        </TableRow>
                                    ))}
                                </TableBody>
                            </Table>

                        </Fragment>
                    </div>
                </header>
            </div>
        );
    }

}

export default Customer;
