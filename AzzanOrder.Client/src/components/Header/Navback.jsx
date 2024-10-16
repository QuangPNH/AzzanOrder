import NavItem from './NavItem';

const Navback = () => {
    return (
        <div className="nav-back">
            <NavItem />
            <style jsx>{`
            .nav-back {
                border-radius: 10px;
                border: var(--sds-size-stroke-border) 0px 0px 0px;
                opacity: var(--sds-size-stroke-border);
                background: #FFFFFFB2;
                border: 1px solid var(--black-100, #000000)
            }
  `     }</style>
        </div>
    );
    
}
export default Navback;
