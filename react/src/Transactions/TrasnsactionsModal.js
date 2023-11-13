import * as React from 'react';
import Backdrop from '@mui/material/Backdrop';
import Box from '@mui/material/Box';
import Modal from '@mui/material/Modal';
import Fade from '@mui/material/Fade';
import Typography from '@mui/material/Typography';
import AccountIcon from "@mui/icons-material/AccountBalance";
import moment from "moment";
import {ListItemButton, ListItemIcon, ListItemText} from "@mui/material";
import {useRef, useState} from "react";

const style = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 400,
    bgcolor: 'background.paper',
    border: '2px solid #000',
    boxShadow: 24,
    p: 4,
};

const TransactionModal = (props) => {
    const [transactions]= useState(props.transactions);

    return (
        <div>
            <Modal
                aria-labelledby="transition-modal-title"
                aria-describedby="transition-modal-description"
                open={props.open}
                onClose={props.handleClose}
                closeAfterTransition
                slots={{backdrop: Backdrop}}
                slotProps={{
                    backdrop: {
                        timeout: 500,
                    },
                }}
            >
                <Fade in={props.open}>
                    <Box sx={style}>
                        <Typography id="transition-modal-title" variant="h6" component="h2">
                            List of transactions
                        </Typography>
                        {transactions &&
                            transactions.map(transaction =>
                                (
                                    <ListItemButton
                                        key={transaction.id}>
                                        <ListItemIcon>
                                            <AccountIcon/>
                                        </ListItemIcon>
                                        <ListItemText
                                            primary={moment(transaction.transactionDate).format('MMMM Do YYYY, h:mm:ss a')}/>
                                    </ListItemButton>
                                )
                            )
                        }
                    </Box>
                </Fade>
            </Modal>
        </div>
    );
}
export default TransactionModal;
