import React, { useState, useEffect } from 'react';
import { getCookie, setCookie } from '../../Account/SignUpForm/Validate';
import API_URLS from '../../../config/apiUrls';

function YNWidgetVoucher({ title, errorTitle, onClose, voucherDetailId }) {

  const [member, setMember] = useState();
  const [voucherDetail, setVoucherDetail] = useState();
  useEffect(() => {
    fetchMember();
    fetchVoucherDetail(voucherDetailId);
  }, []);

  const fetchMember = async () => {
    try {
      const response = await fetch(API_URLS.API + `Member/${JSON.parse(getCookie('memberInfo')).memberId}`);
      const data = await response.json();
      setMember(data);
    } catch (error) {
      console.error('Error adding member voucher:', error);
    }
  };
  const fetchVoucherDetail = async (voucherDetailId) => {
    try {
      const response = await fetch(API_URLS.API + `VoucherDetail/${voucherDetailId}`);
      const data = await response.json();
      setVoucherDetail(data);
    } catch (error) {
      console.error('Error adding member voucher:', error);
    }
  };
  // const addMemberVoucher = (voucherDetailId) => {
  //   const memberVoucher = {
  //       memberId: JSON.parse(getCookie('memberInfo')).memberId,         // Giá trị của memberId
  //       voucherDetailId: voucherDetailId,  // Giá trị của voucherDetailId
  //   };

  //   fetchMemberVoucher(memberVoucher);
  //   console.log(memberVoucher, "thong tin");
  // };

  // const fetchMemberVoucher = async (memberVoucher) => {
  //   try {
  //       const response = await fetch(API_URLS.API + 'MemberVouchers/Add', {
  //           method: 'POST', // Sử dụng phương thức POST để thêm dữ liệu
  //           headers: {
  //               'Content-Type': 'application/json', // Đặt Content-Type là JSON
  //           },
  //           body: JSON.stringify(memberVoucher), // Chuyển đối tượng thành chuỗi JSON
  //       });

  //       if (!response.ok) {
  //           throw new Error('Failed to add member voucher');
  //       }

  //       const data = await response.json();
  //       console.log(data); // Cập nhật danh sách vouchers
  //   } catch (error) {
  //       console.error('Error adding member voucher:', error);
  //   }
  // };
  const apiUrl = API_URLS.API + 'MemberVouchers/Add';
  const handleSubmit = async () => {
    console.log(member.memberId);
    fetch(apiUrl, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        MemberId: member.memberId, // ID thành viên
        VoucherDetailId: voucherDetail.voucherDetailId, // ID chi tiết voucher
    }) // Chuyển đổi đối tượng thành JSON
    })
      .then(response => {
        if (!response.ok) {
          throw new Error('Error in network response');
        }
        return response.json(); // Trả về JSON nếu phản hồi thành công
      })
      .then(result => {
        console.log('Success:', result);
      })
      .catch(error => {
        console.error('Error:', error);
      });
  };

  const handleCancel = () => {
    onClose(); // Close the pop-up
  };

  return (
    <>
      {member && voucherDetail && (

        member.point < voucherDetail.price ?
          <section className="login-widget">
            <form className="login-form" onSubmit={handleCancel}>
              <h2 className="register-title">{errorTitle}</h2>
              <div>
                <button className="submit-button" type="submit">Ok</button>
              </div>
            </form>
          </section> :
          <section className="login-widget">
            <form className="login-form" onSubmit={handleSubmit}>
              <h2 className="register-title">{title}</h2>
              <div>
                <button className="submit-button" type="submit">Yes</button>
                <button className="submit-button" type="reset" onClick={handleCancel}>Cancel </button>
              </div>
            </form>
          </section>

      )}

      <style jsx>{`
        .login-widget {
          border-radius: 0;
          display: flex;
          max-width: 328px;
          flex-direction: row-reverse;
          font-family: Inter, sans-serif;
          color: #000;
        }
        .login-form {
          border-radius: 31px;
          background-color: #fff;
          box-shadow: 0 0 10px rgba(0, 0, 0, 0.25);
          display: flex;
          width: 100%;
          flex-direction: column;
          align-items: center;
          padding: 21px 26px;
        }
        .register-title {
          font-size: 20px;
          font-weight: 700;
          text-align: center;
        }
        .submit-button {
          align-self: center;
          border-radius: 15px;
          border: none;
          background: var(--primary-color);
          margin-top: 15px;
          width: 80px;
          min-height: 35px;
          padding: 5px;
          color: #fff;
          font-size: 16px;
          font-weight: 20;
          text-align: center;
          cursor: pointer;
          transition: background-color 0.3s ease-in-out;
        }
      `}</style>
    </>
  );
}

export default YNWidgetVoucher;